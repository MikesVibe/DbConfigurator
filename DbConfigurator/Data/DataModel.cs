using DbConfigurator.DataAccess;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
        public void Load<T>(T item, string propertName) where T : class
        {
            Context.Entry(item).Reference(c => c.GetType().GetProperty(propertName)).Load();
        }
        public void AddRecipientTo(int distributionInfoId, int recipientId)
        {
            var recipientToAdd = Context.Recipient.Find(recipientId);
            if (recipientToAdd == null)
                return;

            var distributionInfo = Context.DistributionInformation
                .First(d => d.Id == distributionInfoId);

            var toRecipientsGroup = distributionInfo.ToRecipientsGroup;

            if (toRecipientsGroup == null)
            {
                var rg = new RecipientsGroup();
                rg.Recipients = new Collection<Recipient>();
                rg.Name = "TO";
                rg.DistributionInformationId = distributionInfoId;
                Context.RecipientsGroup.Add(rg);
                distributionInfo.ToRecipientsGroup = rg;
                toRecipientsGroup = distributionInfo.ToRecipientsGroup;
            }

            toRecipientsGroup.Recipients.Add(recipientToAdd);


            // Add the existing Recipient entity to the Recipients collection of the first RecipientsGroup
            distributionInfo.ToRecipientsGroup = toRecipientsGroup;
        }

        public void AddRecipientCc(DistributionInformation disInfo, int recipientId)
        {
            var recipientToAdd = Context.Recipient.Find(recipientId);
            if (recipientToAdd == null)
                return;


            var ccRecipientsGroup = disInfo.CcRecipientsGroup;

            if (ccRecipientsGroup == null)
            {
                var rg = new RecipientsGroup();
                rg.Recipients = new Collection<Recipient>();
                rg.Name = "CC";
                rg.DistributionInformationId = disInfo.Id;
                Context.RecipientsGroup.Add(rg);
                disInfo.CcRecipientsGroup = rg;
                ccRecipientsGroup = disInfo.CcRecipientsGroup;
            }

            ccRecipientsGroup.Recipients.Add(recipientToAdd);


            // Add the existing Recipient entity to the Recipients collection of the first RecipientsGroup
            disInfo.CcRecipientsGroup = ccRecipientsGroup;
        }

        public DbConfiguratorDbContext Context
        {
            get { return _context; }
            set { _context = value; }
        }

        private DbConfiguratorDbContext _context;
    }
}
