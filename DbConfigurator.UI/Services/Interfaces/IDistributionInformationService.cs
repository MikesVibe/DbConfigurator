using DbConfigurator.Model.DTOs.Core;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DbConfigurator.UI.Services.Interfaces
{
    public interface IDistributionInformationService : IGenericDataService<DistributionInformationDto>
    {
        Task AddRecipientsCcAsync(int distributionInformationId, IEnumerable<RecipientDto> recipientsCc_ToAdd);
        Task AddRecipientsToAsync(int distributionInformationId, IEnumerable<RecipientDto> recipientsTo_ToAdd);
        Task<IEnumerable<PriorityDto>> GetAllPrioritiesAsync();
        IEnumerable<RecipientDto> GetAllRecipients();
        Task<IEnumerable<AreaDto>> GetAreasAsync();
        Task<IEnumerable<BuisnessUnitDto>> GetBuisnessUnitsAsync(int? areaId = null);
        Task<IEnumerable<CountryDto>> GetCountriesAsync(int? buisnessUnitId = null);
        Task<IEnumerable<RegionDto>> GetRegionsWithAsync(int areaId, int buisnessUnitId, int countryId);
    }
}