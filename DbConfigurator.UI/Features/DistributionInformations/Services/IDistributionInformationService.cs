using DbConfigurator.Model.DTOs.Core;
using DbConfigurator.Model.Entities.Core;
using DbConfigurator.UI.Base.Contracts;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DbConfigurator.UI.Features.DistributionInformations.Services
{
    public interface IDistributionInformationService : IDataService<DistributionInformation>
    {
        //Task AddRecipientsCcAsync(int distributionInformationId, IEnumerable<Recipient> recipientsCc_ToAdd);
        //Task AddRecipientsToAsync(int distributionInformationId, IEnumerable<Recipient> recipientsTo_ToAdd);
        Task<IEnumerable<PriorityDto>> GetAllPrioritiesAsync();
        IEnumerable<Recipient> GetAllRecipients();
        Task<IEnumerable<Area>> GetUniqueAreasFromRegionAsync();
        Task<IEnumerable<BusinessUnit>> GetUniqueBusinessUnitsFromRegionAsync(int? areaId = null);
        Task<IEnumerable<Country>> GetUniqueCountriesFromRegionAsync(int? areaId = null, int? BusinessUnitId = null);
        Task<IEnumerable<Region>> GetRegionsWithAsync(int areaId, int BusinessUnitId, int countryId);
        Task<IEnumerable<Region>> GetAllRegionsAsync();
        Task<IEnumerable<Recipient>> GetAllRecipientsAsync();
    }
}