using DbConfigurator.Model.DTOs.Core;
using DbConfigurator.Model.Entities.Core;
using DbConfigurator.UI.Repositories;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DbConfigurator.UI.Services
{
    public interface IDataService
    {
        Task SaveChangesAsync();


        RegionsRepository RegionsRepository { get; }

        Task AddAsync<T>(T item) where T : class;
        Task<Recipient> GetRecipientByIdAsync(int id);
        void Remove<T>(T item) where T : class;
        void SaveChanges();
        void Add<T>(T item) where T : class;
        Task<DistributionInformation> GetDistributionInformationByIdAsync(int id);
        Task<ICollection<Recipient>> GetAllRecipientsAsync();
        Task<ICollection<Area>> GetAllAreasAsync();
        Task<ICollection<BuisnessUnit>> GetAllBuisnessUnitsAsync();
        Task<ICollection<Country>> GetAllCountriesAsync();
        Task<ICollection<Region>> GetAllRegionsAsync();
        Task<Region?> GetRegionAsync(int areaId, int buisnessUnitId, int countryId);
        Region? GetRegionById(int id);

        Area? GetAreaById(int id);
        BuisnessUnit? GetBuisnessUnitById(int id);
        Country? GetCountryById(int id);
        Task<ICollection<Priority>> GetAllPrioritiesAsync();
        IEnumerable<Recipient> GetAllRecipients();
        Task<IEnumerable<DistributionInformationDto>> GetAllDistributionInformationDtoAsync();
    }
}