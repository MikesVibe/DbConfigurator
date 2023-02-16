using DbConfigurator.DataAccess;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DbConfigurator.Model
{
    public class DataModel : IDataModel
    {
        public DataModel(
            DbConfiguratorDbContext dbConfiguratorDbContext
            )
        {
            Context = dbConfiguratorDbContext;



            LoadDataFromDatabase();
        }

        private async void LoadDataFromDatabase()
        {
            DistributionInformations = await GetAllDistributionInformationAsync();
            Areas = await GetAllAreasAsync();
            BuisnessUnits = await GetAllBuisnessUnitsAsync();
            Countries = await GetAllCountriesAsync();
            Priorities = await GetAllPrioritiesAsync();
            Recipients = await GetAllRecipientsAsync();

        }

        public void SaveChangesAsync()
        {
            Context.SaveChangesAsync();
        }

        private async Task<IEnumerable<DistributionInformation>> GetAllDistributionInformationAsync()
        {
            var collection = await Context.Set<DistributionInformation>()
                .Include(c => c.Country).ThenInclude(c => c.BuisnessUnits).ThenInclude(bu => bu.Areas)
                .Include(c => c.ToRecipientsGroup)
                .ThenInclude(t => t.Recipients)
                .Include(c => c.CcRecipientsGroup)
                .ThenInclude(t =>t.Recipients)
                .Include(p => p.Priority)
                .ToListAsync();

            return collection;
        }
        private async Task<IEnumerable<Area>> GetAllAreasAsync()
        {
            var collection = await Context.Set<Area>().AsNoTracking().ToListAsync();

            return collection;
        }
        private async Task<IEnumerable<BuisnessUnit>> GetAllBuisnessUnitsAsync()
        {
            var collection = await Context.Set<BuisnessUnit>().AsNoTracking().ToListAsync();

            return collection;
        }
        private async Task<IEnumerable<Country>> GetAllCountriesAsync()
        {
            var collection = await Context.Set<Country>().Include(c => c.BuisnessUnits).ThenInclude(bu => bu.Areas).AsNoTracking().ToListAsync();

            return collection;
        }
        private async Task<IEnumerable<Priority>> GetAllPrioritiesAsync()
        {
            var collection = await Context.Set<Priority>().AsNoTracking().ToListAsync();
            return collection;
        }
        private async Task<IEnumerable<Recipient>> GetAllRecipientsAsync()
        {
            var collection = await Context.Set<Recipient>().AsNoTracking().ToListAsync();
            return collection;
        }
        public IEnumerable<DistributionInformation> DistributionInformations { get; set; }
        public IEnumerable<Area> Areas { get; set; }
        public IEnumerable<BuisnessUnit> BuisnessUnits { get; set; }
        public IEnumerable<Country> Countries { get; set; }
        public IEnumerable<Priority> Priorities { get; set; }
        public IEnumerable<Recipient> Recipients { get; set; }

        public bool HasChanges()
        {
            return Context.ChangeTracker.HasChanges();
        }

        public void ReloadEntryPriority(DistributionInformation disInfo)
        {
            Context.Entry(disInfo).Reference(e => e.Priority).Load();
        }

        public void ReloadEntryCountry(DistributionInformation disInfo)
        {
            Context.Entry(disInfo).Reference(d => d.Country).Load();
            Context.Entry(disInfo.Country).Collection(c => c.BuisnessUnits).Load();
            foreach (var buisnessUnit in disInfo.Country.BuisnessUnits)
            {
                Context.Entry(buisnessUnit).Collection(bu => bu.Areas).Load();
            }
        }

        public void Add<T>(T item) where T : class
        {
            Context.Set<T>().Add(item);
        }

        public void AddRecipientTo(int id, Recipient value)
        {
            var existingRecipient = Context.Recipient.FirstOrDefault(r => r.Id == value.Id);

            var distributionInfo = Context.DistributionInformation
                .Include(di => di.ToRecipientsGroup)
                .ThenInclude(rg => rg.Recipients)
                .Include(di => di.CcRecipientsGroup)
                .ThenInclude(rg => rg.Recipients)
                .FirstOrDefault();

            // Get the first RecipientsGroup in the collection
            var firstGroup = distributionInfo.ToRecipientsGroup;

            // Add the existing Recipient entity to the Recipients collection of the first RecipientsGroup
            firstGroup.Recipients.Add(existingRecipient);
        }

        public DbConfiguratorDbContext Context
        {
            get { return _context; }
            set { _context = value; }
        }

        private DbConfiguratorDbContext _context;
    }
}
