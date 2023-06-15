using DbConfigurator.DataAccess;
using DbConfigurator.Model.DTOs.Core;
using DbConfigurator.Model.Entities.Core;
using DbConfigurator.UI.Startup;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
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
            DefaultRegion = await GetDefaultRegion();

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
        public void SaveChanges()
        {
            Context.SaveChanges();
        }

        public async Task<ICollection<DistributionInformation>> GetAllDistributionInformationAsync()
        {
            var collection = await Context.Set<DistributionInformation>()
                //.Include(c => c.Country).ThenInclcsdsude(c => c.BuisnessUnits).ThenInclude(bu => bu.Areas)
                .Include(d => d.Region).ThenInclude(r => r.Area)
                .Include(d => d.Region).ThenInclude(r => r.BuisnessUnit)
                .Include(d => d.Region).ThenInclude(r => r.Country)
                .Include(d => d.RecipientsTo)
                .Include(d => d.RecipientsCc)
                .Include(d => d.Priority)
                .ToListAsync();

            return collection;
        }
        public async Task<ICollection<Region>> GetAllRegionsAsync()
        {
            var collection = await GetRegionsAsQueryable().OrderBy(r => r.Id).ToListAsync();

            return collection;
        }
        public async Task<ICollection<Area>> GetAllAreasAsync()
        {
            var collection = await Context.Set<Area>().AsNoTracking().ToListAsync();

            return collection;
        }
        public async Task<ICollection<BuisnessUnit>> GetAllBuisnessUnitsAsync()
        {
            var collection = await Context.Set<BuisnessUnit>().AsNoTracking().ToListAsync();

            return collection;
        }
        private async Task<ICollection<Priority>> GetAllPrioritiesAsync()
        {
            var collection = await Context.Set<Priority>().AsNoTracking().ToListAsync();
            return collection;
        }
        public async Task<ICollection<Recipient>> GetAllRecipientsAsync()
        {
            var collection = await Context.Set<Recipient>().AsNoTracking().ToListAsync();
            return collection;
        }

        public async Task<ICollection<Country>> GetAllCountriesAsync()
        {
            var collection = await Context.Set<Country>().ToListAsync();
            //.Include(c => c.BuisnessUnits).ThenInclude(bu => bu.Areas).AsNoTracking().ToListAsync();

            return collection;
        }
        public async Task<ICollection<Area>> GetAreasWithoutDefaultAsync()
        {
            var collection = await Context.Set<Area>().AsNoTracking().Where(a => a.Id != DefaultArea.Id).ToListAsync();

            return collection;
        }
        public async Task<ICollection<BuisnessUnit>> GetBuisnessUnitsWithoutDefaultAsync()
        {
            var collection = await Context.Set<BuisnessUnit>().AsNoTracking().Where(a => a.Id != DefaultBuisnessUnit.Id).ToListAsync();

            return collection;
        }
        public async Task<ICollection<Country>> GetCountriesWithoutDefaultAsync()
        {
            var collection = await Context.Set<Country>()
                //.Include(c => c.BuisnessUnits)
                //.ThenInclude(bu => bu.Areas)
                .AsNoTracking().Where(c => c.Id != DefaultCountry.Id).ToListAsync();

            return collection;
        }
        public async Task<Region?> GetRegionAsync(int areaId, int buisnessUnitId, int countryId)
        {
            return await GetRegionsAsQueryable()
                .Where(r =>
                r.AreaId == areaId &&
                r.BuisnessUnitId == buisnessUnitId &&
                r.CountryId == countryId).FirstOrDefaultAsync();
        }
        public async Task<IEnumerable<BuisnessUnit>> GetBuisnessUnitsAsync(int areaId)
        {
            var regionsWithAreaId = GetRegionsAsQueryable().Where(i => i.AreaId == areaId);
            var buisnessUnitsIdList = await regionsWithAreaId.Select(r => r.BuisnessUnitId).ToListAsync();

            var buisnessUnits = GetBuisnessUnitsAsQueryable().Where(b => buisnessUnitsIdList.Contains(b.Id));

            return await buisnessUnits.ToListAsync();
        }
        public async Task<IEnumerable<Country>> GetCountriesAsync(int buisnessUnitId)
        {
            var regionsWithBuisnessUnitId = GetRegionsAsQueryable().Where(i => i.BuisnessUnitId == buisnessUnitId);

            var testList = regionsWithBuisnessUnitId.ToList();
            var countriesIdList = await regionsWithBuisnessUnitId.Select(r => r.CountryId).ToListAsync();

            var countries = GetCountriesAsQueryable().Where(c => countriesIdList.Contains(c.Id));

            return await countries.ToListAsync();
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
        private async Task<Region> GetDefaultRegion()
        {
            var region = await Context.Set<Region>().Where(c => c.Id == 99)
                .FirstOrDefaultAsync();
            return region;
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
        public Region DefaultRegion { get; private set; }


        public bool HasChanges()
        {
            return Context.ChangeTracker.HasChanges();
        }


        public async Task AddAsync<T>(T item) where T : class
        {
            await Context.Set<T>().AddAsync(item);
        }
        public void Add<T>(T item) where T : class
        {
            Context.Set<T>().Add(item);
        }
        public void Remove<T>(T item) where T : class
        {
            Context.Set<T>().Remove(item);
        }
        public async Task<Recipient> GetRecipientByIdAsync(int id)
        {
            return await Context.Recipient.Where(r => r.Id == id).FirstAsync();
        }
        public async Task<DistributionInformation> GetDistributionInformationByIdAsync(int id)
        {
            return await Context.DistributionInformation.Where(d => d.Id == id)
                //.Include(c => c.Country).ThenInclude(c => c.BuisnessUnits).ThenInclude(bu => bu.Areas)
                .Include(r => r.Region)
                .Include(t => t.RecipientsTo)
                .Include(t => t.RecipientsCc)
                .Include(p => p.Priority)
                .FirstAsync();
        }
        public Region? GetRegionById(int id)
        {
            return GetRegionsAsQueryable().Where(r => r.Id == id).FirstOrDefault();
        }
        public Area? GetAreaById(int id)
        {
            return _context.Area.Where(a => a.Id == id).FirstOrDefault();
        }
        public BuisnessUnit? GetBuisnessUnitsById(int id)
        {
            return _context.BuisnessUnit.Where(a => a.Id == id).FirstOrDefault();
        }

        public bool IsDefaultCountry(int countryId)
        {
            return DefaultCountry.Id == countryId;
        }

        public async Task<IEnumerable<Region>> GetRegionsByAreaIdAsync(int id)
        {
            return await Context.Set<Region>().Where(r => r.Area.Id == id).ToListAsync();
        }
        private IQueryable<Region> GetRegionsAsQueryable()
        {
            return Context.Set<Region>()
                .Include(r => r.Area)
                .Include(r => r.BuisnessUnit)
                .Include(r => r.Country)
                .AsQueryable();
        }
        private IQueryable<BuisnessUnit> GetBuisnessUnitsAsQueryable()
        {
            return Context.Set<BuisnessUnit>()
                .AsQueryable();
        }
        private IQueryable<Country> GetCountriesAsQueryable()
        {
            return Context.Set<Country>()
                .AsQueryable();
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
