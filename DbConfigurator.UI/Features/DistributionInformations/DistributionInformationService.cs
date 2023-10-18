using DbConfigurator.DataAccess.Repositories;
using DbConfigurator.DataAccess.Repository;
using DbConfigurator.Model.DTOs.Core;
using DbConfigurator.Model.Entities.Core;
using DbConfigurator.UI.Services;
using DbConfigurator.UI.Services.Interfaces;
using DbConfigurator.UI.Startup;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DbConfigurator.UI.Features.DistributionInformations
{
    public class DistributionInformationService : GenericDataService<DistributionInformation, DistributionInformationDto, DistributionInformationRepository>, IDistributionInformationService
    {
        private readonly RecipientRepository _recipientRepository;
        private readonly PriorityRepository _priorityRepository;
        private readonly RegionRepository _regionRepository;
        private readonly BusinessUnitRepository _BusinessUnitRepository;
        private readonly CountryRepository _countryRepository;

        public DistributionInformationService(
            DistributionInformationRepository repository,
            RecipientRepository recipientRepository,
            PriorityRepository priorityRepository,
            RegionRepository regionRepository,
            BusinessUnitRepository BusinessUnitRepository,
            CountryRepository countryRepository,
            AutoMapperConfig autoMapper
            )
            : base(repository, autoMapper)
        {
            _recipientRepository = recipientRepository;
            _priorityRepository = priorityRepository;
            _regionRepository = regionRepository;
            _BusinessUnitRepository = BusinessUnitRepository;
            _countryRepository = countryRepository;
        }



        public async Task<IEnumerable<AreaDto>> GetUniqueAreasFromRegionAsync()
        {
            var countries = await _regionRepository.GetUniqueAreasFromRegionAsync();

            return _autoMapper.Mapper.Map<IEnumerable<AreaDto>>(countries);
        }
        public async Task<IEnumerable<BusinessUnitDto>> GetUniqueBusinessUnitsFromRegionAsync(int? areaId = null)
        {
            IEnumerable<Region> regions = areaId is null ?
                await _regionRepository.GetAllAsync() :
                await _regionRepository.GetAllAsync(i => i.AreaId == areaId);

            var BusinessUnitsIdList = regions.Select(r => r.BusinessUnitId);
            var BusinessUnits = await _BusinessUnitRepository.GetAllAsync(b => BusinessUnitsIdList.Contains(b.Id));

            return _autoMapper.Mapper.Map<IEnumerable<BusinessUnitDto>>(BusinessUnits);
        }
        public async Task<IEnumerable<CountryDto>> GetUniqueCountriesFromRegionAsync(int? areaId = null, int? BusinessUnitId = null)
        {
            var regions = BusinessUnitId is null ?
                await _regionRepository.GetAllAsync() :
                await _regionRepository.GetAllAsync(i => i.AreaId == areaId && i.BusinessUnitId == BusinessUnitId);

            var countriesIdList = regions.Select(r => r.CountryId);
            var countries = await _countryRepository.GetAllAsync(c => countriesIdList.Contains(c.Id));

            return _autoMapper.Mapper.Map<IEnumerable<CountryDto>>(countries);
        }
        public IEnumerable<RecipientDto> GetAllRecipients()
        {
            var recipients = _recipientRepository.GetAll();

            return _autoMapper.Mapper.Map<IEnumerable<RecipientDto>>(recipients);
        }
        public async Task<IEnumerable<RecipientDto>> GetAllRecipientsAsync()
        {
            var recipients = await _recipientRepository.GetAllAsync();

            return _autoMapper.Mapper.Map<IEnumerable<RecipientDto>>(recipients);
        }
        public async Task<IEnumerable<PriorityDto>> GetAllPrioritiesAsync()
        {
            var priorities = await _priorityRepository.GetAllAsync();

            return _autoMapper.Mapper.Map<IEnumerable<PriorityDto>>(priorities);
        }
        public async Task<IEnumerable<RegionDto>> GetRegionsWithAsync(int areaId, int BusinessUnitId, int countryId)
        {
            var regions = await _regionRepository.GetRegionsWithAsync(areaId, BusinessUnitId, countryId);

            return _autoMapper.Mapper.Map<IEnumerable<RegionDto>>(regions);
        }
        public async Task<IEnumerable<RegionDto>> GetAllRegionsAsync()
        {
            var regions = await _regionRepository.GetAllAsync();

            return _autoMapper.Mapper.Map<IEnumerable<RegionDto>>(regions);
        }
        public override async Task<IEnumerable<DistributionInformationDto>> GetAllAsync()
        {
            var distributionInformations = await _repository.GetAllAsync();

            return _autoMapper.Mapper.Map<IEnumerable<DistributionInformationDto>>(distributionInformations);
        }

    }
}
