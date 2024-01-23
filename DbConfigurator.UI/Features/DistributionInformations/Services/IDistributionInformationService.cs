using DbConfigurator.DataAccess.DTOs.PriorityDtos;
using DbConfigurator.Core.Models;
using DbConfigurator.UI.Base.Contracts;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DbConfigurator.UI.Features.DistributionInformations.Services
{
    public interface IDistributionInformationService : IDataService<DistributionInformation>
    {
        //Task AddRecipientsCcAsync(int distributionInformationId, IEnumerable<Recipient> recipientsCc_ToAdd);
        //Task AddRecipientsToAsync(int distributionInformationId, IEnumerable<Recipient> recipientsTo_ToAdd);
        Task<IEnumerable<Priority>> GetAllPrioritiesAsync();
        Task<IEnumerable<Area>> GetAllAreaFiltersForRegionAsync();
        Task<IEnumerable<BusinessUnit>> GetAllBusinessUnitFiltersForRegionAsync(int? areaId = null);
        Task<IEnumerable<Country>> GetCountryFiltersForRegionAsync(int? areaId = null, int? BusinessUnitId = null);
        Task<IEnumerable<Region>> GetRegionsWithAsync(int areaId, int BusinessUnitId, int countryId);
        Task<IEnumerable<Region>> GetAllRegionsAsync();
        Task<IEnumerable<Recipient>> GetAllRecipientsAsync();
    }
}