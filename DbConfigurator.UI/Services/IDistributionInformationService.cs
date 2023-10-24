using DbConfigurator.DataAccess.DTOs.AreaDto;
using DbConfigurator.Model.DTOs.Core;
using DbConfigurator.Model.Entities.Core;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DbConfigurator.UI.Services
{
    public interface IDistributionInformationService : IDataService<CreateAreaDto, UpdateAreaDto, Area>
    {
        //Task AddRecipientsCcAsync(int distributionInformationId, IEnumerable<RecipientDto> recipientsCc_ToAdd);
        //Task AddRecipientsToAsync(int distributionInformationId, IEnumerable<RecipientDto> recipientsTo_ToAdd);
        Task<IEnumerable<PriorityDto>> GetAllPrioritiesAsync();
        IEnumerable<RecipientDto> GetAllRecipients();
        Task<IEnumerable<AreaDto>> GetUniqueAreasFromRegionAsync();
        Task<IEnumerable<BusinessUnitDto>> GetUniqueBusinessUnitsFromRegionAsync(int? areaId = null);
        Task<IEnumerable<CountryDto>> GetUniqueCountriesFromRegionAsync(int? areaId = null, int? BusinessUnitId = null);
        Task<IEnumerable<RegionDto>> GetRegionsWithAsync(int areaId, int BusinessUnitId, int countryId);
        Task<IEnumerable<RegionDto>> GetAllRegionsAsync();
        Task<IEnumerable<RecipientDto>> GetAllRecipientsAsync();
    }
}