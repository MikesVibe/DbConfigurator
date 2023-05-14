using DbConfigurator.DataAccess;
using DbConfigurator.Model.DTOs;
using DbConfigurator.UI.Startup;
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
            DbConfiguratorDbContext dbConfiguratorDbContext,
            AutoMapperConfig autoMapperConfig
            )
        {
            Context = dbConfiguratorDbContext;
            AutoMapper = autoMapperConfig;


            LoadDataFromDatabase();
        }

        private async void LoadDataFromDatabase()
        {
            Areas = await GetAllAreasAsync();
            BuisnessUnits = await GetAllBuisnessUnitsAsync();
            Countries = await GetAllCountriesAsync();
            Priorities = await GetAllPrioritiesAsync();
            Recipients = await GetAllRecipientsAsync();
            DefaultArea = await GetDefaultArea();
            DefaultBuisnessUnit = await GetDefaultBuisnessUnit();
            DefaultCountry = await GetDefaultCountry();
            DefaultPriority = await GetDefaultPriority();

            AreasDto = AutoMapper.Mapper.Map<List<AreaDto>>(await GetAllAreasAsync());
            BuisnessUnitsDto = AutoMapper.Mapper.Map<List<BuisnessUnitDto>>(await GetAllBuisnessUnitsAsync());
            CountriesDto = AutoMapper.Mapper.Map<List<CountryDto>>(await GetAllCountriesAsync());
            PrioritiesDto = AutoMapper.Mapper.Map<List<PriorityDto>>(await GetAllPrioritiesAsync());
            RecipientsDto = AutoMapper.Mapper.Map<List<RecipientDto>>(await GetAllRecipientsAsync());
        }

        public async Task SaveChangesAsync()
        {
            await Context.SaveChangesAsync();
        }
        public async Task<ICollection<DistributionInformation>> GetAllDistributionInformationAsync()
        {
            var collection = await Context.Set<DistributionInformation>()
                .Include(c => c.Country).ThenInclude(c => c.BuisnessUnits).ThenInclude(bu => bu.Areas)
                .Include(t => t.RecipientsTo)
                .Include(t =>t.RecipientsCc)
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


        public ICollection<DistributionInformationDto> DistributionInformationsDto { get; private set; }
        public ICollection<AreaDto> AreasDto { get; private set; }
        public ICollection<BuisnessUnitDto> BuisnessUnitsDto { get; private set; }
        public ICollection<CountryDto> CountriesDto { get; private set; }
        public ICollection<PriorityDto> PrioritiesDto { get; private set; }
        public ICollection<RecipientDto> RecipientsDto { get; private set; }

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

        public async Task ReloadEntryPriorityAsync(DistributionInformation disInfo)
        {
            await Context.Entry(disInfo).Reference(e => e.Priority).LoadAsync();
        }

        public async Task ReloadEntryCountryAsync(DistributionInformation disInfo)
        {
            await Context.Entry(disInfo).Reference(d => d.Country).LoadAsync();
            await Context.Entry(disInfo.Country).Collection(c => c.BuisnessUnits).LoadAsync();
            foreach (var buisnessUnit in disInfo.Country.BuisnessUnits)
            {
                await Context.Entry(buisnessUnit).Collection(bu => bu.Areas).LoadAsync();
            }
        }

        public async Task AddAsync<T>(T item) where T : class
        {
            await Context.Set<T>().AddAsync(item);
        }
        public void Remove<T>(T item) where T : class
        {
            Context.Set<T>().Remove(item);
        }
        public async Task<Recipient> GetRecipientAsync(int id)
        {
            return await Context.Recipient.Where(r => r.Id == id).FirstAsync();
        }

        public async Task<DistributionInformation> GetDistributionInformationByIdAsync(int id)
        {
            return await Context.DistributionInformation.Where(d => d.Id == id)
                .Include(c => c.Country).ThenInclude(c => c.BuisnessUnits).ThenInclude(bu => bu.Areas)
                .Include(t => t.RecipientsTo)
                .Include(t => t.RecipientsCc)
                .Include(p => p.Priority)
                .FirstAsync();
        }

        public bool IsDefaultCountry(int countryId)
        {
            return DefaultCountry.Id == countryId;
        }

        public DbConfiguratorDbContext Context
        {
            get { return _context; }
            set { _context = value; }
        }

        private AutoMapperConfig AutoMapper { get; set; }


        private DbConfiguratorDbContext _context;
    }
}
