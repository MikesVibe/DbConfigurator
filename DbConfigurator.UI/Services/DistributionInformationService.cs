using DbConfigurator.DataAccess.Repositories;
using DbConfigurator.DataAccess.Repository;
using DbConfigurator.Model.DTOs.Core;
using DbConfigurator.Model.Entities.Core;
using DbConfigurator.UI.Services.Interfaces;
using DbConfigurator.UI.Startup;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DbConfigurator.UI.Services
{
    public class DistributionInformationService : GenericDataService<DistributionInformation, DistributionInformationDto, DistributionInformationRepository>, IDistributionInformationService
    {
        private readonly RecipientRepository _recipientRepository;
        private readonly PriorityRepository _priorityRepository;
        private readonly RegionRepository _regionRepository;

        public DistributionInformationService(
            DistributionInformationRepository repository,
            RecipientRepository recipientRepository,
            PriorityRepository priorityRepository,
            RegionRepository regionRepository,
            AutoMapperConfig autoMapper)
            : base(repository, autoMapper)
        {
            _recipientRepository = recipientRepository;
            _priorityRepository = priorityRepository;
            _regionRepository = regionRepository;
        }

        public async Task<IEnumerable<AreaDto>> GetAreasAsync()
        {
            var countries = await _repository.GetAreasAsync();

            return _autoMapper.Mapper.Map<IEnumerable<AreaDto>>(countries);
        }
        public async Task<IEnumerable<BuisnessUnitDto>> GetBuisnessUnitsAsync(int? areaId = null)
        {
            var buisnessUnits = await _repository.GetBuisnessUnitsAsync();

            return _autoMapper.Mapper.Map<IEnumerable<BuisnessUnitDto>>(buisnessUnits);
        }
        public async Task<IEnumerable<CountryDto>> GetCountriesAsync(int? buisnessUnitId = null)
        {
            await Task.CompletedTask;
            var countries = await _repository.GetCountriesAsync();

            return _autoMapper.Mapper.Map<IEnumerable<CountryDto>>(countries);
        }

        public IEnumerable<RecipientDto> GetAllRecipients()
        {
            var recipients = _recipientRepository.GetAll();

            return _autoMapper.Mapper.Map<IEnumerable<RecipientDto>>(recipients);
        }

        public async Task<IEnumerable<PriorityDto>> GetAllPrioritiesAsync()
        {
            var priorities = await _priorityRepository.GetAllAsync();

            return _autoMapper.Mapper.Map<IEnumerable<PriorityDto>>(priorities);
        }

        public async Task<IEnumerable<RegionDto>> GetRegionsWithAsync(int areaId, int buisnessUnitId, int countryId)
        {
            var regions = await _regionRepository.GetRegionsWithAsync(areaId, buisnessUnitId, countryId);

            return _autoMapper.Mapper.Map<IEnumerable<RegionDto>>(regions);
        }
    }
}
