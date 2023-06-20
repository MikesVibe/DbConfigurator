using DbConfigurator.Model.DTOs.Core;
using DbConfigurator.Model.Entities.Core;
using DbConfigurator.UI.Data;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DbConfigurator.Model
{
    public interface IDataModel
    {
        Task SaveChangesAsync();

        ICollection<Area> Areas { get; }
        ICollection<BuisnessUnit> BuisnessUnits { get; }
        ICollection<Country> Countries { get; }
        ICollection<Priority> Priorities { get; }
        ICollection<Recipient> Recipients { get; }
        Country DefaultCountry { get; }
        Priority DefaultPriority { get; }
        Area DefaultArea { get; }
        Region DefaultRegion { get; }
        BuisnessUnit DefaultBuisnessUnit { get; }
        ICollection<AreaDto> AreasDto { get; }
        ICollection<BuisnessUnitDto> BuisnessUnitsDto { get; }
        ICollection<CountryDto> CountriesDto { get; }
        ICollection<PriorityDto> PrioritiesDto { get; }
        ICollection<RecipientDto> RecipientsDto { get; }
        RegionsRepository RegionsRepository { get; set; }

        bool HasChanges();

        Task AddAsync<T>(T item) where T : class;
        Task<Recipient> GetRecipientByIdAsync(int id);

        void Remove<T>(T item) where T : class;
        Task<ICollection<DistributionInformation>> GetAllDistributionInformationAsync();
        Task<DistributionInformation> GetDistributionInformationByIdAsync(int id);
        bool IsDefaultCountry(int countryId);
        Task<ICollection<Recipient>> GetAllRecipientsAsync();
        Task<ICollection<Area>> GetAllAreasAsync();
        Task<ICollection<BuisnessUnit>> GetAllBuisnessUnitsAsync();
        Task<ICollection<Country>> GetAllCountriesAsync();
        Task<ICollection<Area>> GetAreasWithoutDefaultAsync();
        Task<ICollection<BuisnessUnit>> GetBuisnessUnitsWithoutDefaultAsync();
        Task<ICollection<Country>> GetCountriesWithoutDefaultAsync();
        Task<IEnumerable<Region>> GetRegionsByAreaIdAsync(int id);

        Task<IEnumerable<BuisnessUnit>> GetBuisnessUnitsAsync(int areaId);
        Task<IEnumerable<Country>> GetCountriesAsync(int buisnessUnitId);
        Task<ICollection<Region>> GetAllRegionsAsync();
        Task<Region?> GetRegionAsync(int areaId, int buisnessUnitId, int countryId);
        Region? GetRegionById(int id);
        void SaveChanges();
        void Add<T>(T item) where T : class;
        Area? GetAreaById(int id);
        BuisnessUnit? GetBuisnessUnitById(int id);
        Country? GetCountryById(int id);
        Task<List<RegionDto>> GetAllRegionsDtoAsync();
    }
}