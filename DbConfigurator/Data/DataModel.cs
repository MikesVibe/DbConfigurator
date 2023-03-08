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
            DefaultArea = await GetDefaultArea();
            DefaultBuisnessUnit = await GetDefaultBuisnessUnit();
            DefaultCountry = await GetDefaultCountry();
            DefaultPriority = await GetDefaultPriority();

        }

        public async Task SaveChangesAsync()
        {
            await Context.SaveChangesAsync();
        }
        private async Task<ICollection<DistributionInformation>> GetAllDistributionInformationAsync()
        {
            var collection = await Context.Set<DistributionInformation>()
                .Include(c => c.Country).ThenInclude(c => c.BuisnessUnits).ThenInclude(bu => bu.Areas)
                .Include(c => c.RecipientsGroup)
                .ThenInclude(t => t.RecipientsTo)
                .Include(c => c.RecipientsGroup)
                .ThenInclude(t =>t.RecipientsCc)
                .Include(p => p.Priority)
                .ToListAsync();

            return collection;
        }
        private async Task<ICollection<Area>> GetAllAreasAsync()
        {
            var collection = await Context.Set<Area>().AsNoTracking().ToListAsync();

            return collection;
        }
        private async Task<ICollection<BuisnessUnit>> GetAllBuisnessUnitsAsync()
        {
            var collection = await Context.Set<BuisnessUnit>().AsNoTracking().ToListAsync();

            return collection;
        }
        private async Task<ICollection<Country>> GetAllCountriesAsync()
        {
            var collection = await Context.Set<Country>().Include(c => c.BuisnessUnits).ThenInclude(bu => bu.Areas).AsNoTracking().ToListAsync();

            return collection;
        }
        private async Task<ICollection<Priority>> GetAllPrioritiesAsync()
        {
            var collection = await Context.Set<Priority>().AsNoTracking().ToListAsync();
            return collection;
        }
        private async Task<ICollection<Recipient>> GetAllRecipientsAsync()
        {
            var collection = await Context.Set<Recipient>().AsNoTracking().ToListAsync();
            return collection;
        }
        private async Task<Area> GetDefaultArea()
        {
            var area = await Context.Set<Area>().Where(a => a.Id == 99)
                .FirstOrDefaultAsync();
            return area;
        }
        private async Task<BuisnessUnit> GetDefaultBuisnessUnit()
        {
            var buisnessUnit = await Context.Set<BuisnessUnit>().Where(bu => bu.Id == 99)
                .FirstOrDefaultAsync();
            return buisnessUnit;
        }
        private async Task<Country> GetDefaultCountry()
        {
            var country = await Context.Set<Country>().Where(c => c.Id == 99)
                .FirstOrDefaultAsync();
            return country;
        }
        private async Task<Priority> GetDefaultPriority()
        {
            var priority = await Context.Set<Priority>().Where(c => c.Id == 99)
                .FirstOrDefaultAsync();
            return priority;
        }

        public ICollection<DistributionInformation> DistributionInformations { get; private set; }
        public ICollection<Area> Areas { get; private set; }
        public ICollection<BuisnessUnit> BuisnessUnits { get; private set; }
        public ICollection<Country> Countries { get; private set; }
        public ICollection<Priority> Priorities { get; private set; }
        public ICollection<Recipient> Recipients { get; private set; }
        public Area DefaultArea { get; private set; }
        public BuisnessUnit DefaultBuisnessUnit { get; private set; }
        public Country DefaultCountry { get; private set; }
        public Priority DefaultPriority { get; private set; }

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
        public async Task AddAsync<T>(T item) where T : class
        {
            await Context.Set<T>().AddAsync(item);
        }
        public void Load<T>(T item, string propertName) where T : class
        {
            Context.Entry(item).Reference(c => c.GetType().GetProperty(propertName)).Load();
        }
        public async Task ReloadEntityAsync(DistributionInformation item)
        {
            await Context.Entry(item).Reference(d => d.Area).LoadAsync();
            await Context.Entry(item).Reference(d => d.BuisnessUnit).LoadAsync();
            await Context.Entry(item).Reference(d => d.Country).LoadAsync();
            await Context.Entry(item).Reference(d => d.Priority).LoadAsync();
            await Context.Entry(item).Reference(d => d.RecipientsGroup).LoadAsync();
        }

        public void AddRecipientTo(int distributionInfoId, int recipientId)
        {
            //var recipientToAdd = Context.Recipient.Find(recipientId);
            //if (recipientToAdd == null)
            //    return;

            //var distributionInfo = Context.DistributionInformation
            //    .First(d => d.Id == distributionInfoId);

            //var toRecipientsGroup = distributionInfo.ToRecipientsGroup;

            ////if (toRecipientsGroup == null)
            ////{
            ////    var rg = new RecipientsGroup();
            ////    rg.Recipients = new Collection<Recipient>();
            ////    rg.DistributionInformationId = distributionInfoId;
            ////    Context.RecipientsGroup.Add(rg);
            ////    distributionInfo.ToRecipientsGroup = rg;
            ////    toRecipientsGroup = distributionInfo.ToRecipientsGroup;
            ////}

            //toRecipientsGroup.Recipients.Add(recipientToAdd);


            //// Add the existing Recipient entity to the Recipients collection of the first RecipientsGroup
            //distributionInfo.ToRecipientsGroup = toRecipientsGroup;
        }

        public void AddRecipientCc(DistributionInformation disInfo, int recipientId)
        {
            //var recipientToAdd = Context.Recipient.Find(recipientId);
            //if (recipientToAdd == null)
            //    return;


            //var ccRecipientsGroup = disInfo.CcRecipientsGroup;

            ////if (ccRecipientsGroup == null)
            ////{
            ////    var rg = new RecipientsGroup();
            ////    rg.Recipients = new Collection<Recipient>();
            ////    rg.DistributionInformationId = disInfo.Id;
            ////    Context.RecipientsGroup.Add(rg);
            ////    disInfo.CcRecipientsGroup = rg;
            ////    ccRecipientsGroup = disInfo.CcRecipientsGroup;
            ////}

            //ccRecipientsGroup.Recipients.Add(recipientToAdd);


            //// Add the existing Recipient entity to the Recipients collection of the first RecipientsGroup
            //disInfo.CcRecipientsGroup = ccRecipientsGroup;
        }



        public DbConfiguratorDbContext Context
        {
            get { return _context; }
            set { _context = value; }
        }


        private DbConfiguratorDbContext _context;
    }
}
