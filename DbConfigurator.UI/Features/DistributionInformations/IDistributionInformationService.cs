﻿using DbConfigurator.Model.DTOs.Core;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DbConfigurator.UI.Services.Interfaces
{
    public interface IDistributionInformationService : IDataService<DistributionInformationDto>
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