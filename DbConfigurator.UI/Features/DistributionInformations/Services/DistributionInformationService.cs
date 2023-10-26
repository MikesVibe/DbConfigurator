using DbConfigurator.DataAccess;
using DbConfigurator.DataAccess.DTOs.BusinessUnitDtos;
using DbConfigurator.DataAccess.DTOs.DistributionInformationDtos;
using DbConfigurator.DataAccess.DTOs.PriorityDtos;
using DbConfigurator.Model.Entities.Core;
using DbConfigurator.UI.Base;
using DbConfigurator.UI.Features.Areas.Services;
using DbConfigurator.UI.Features.BuisnessUnits.Services;
using DbConfigurator.UI.Features.Countries.Services;
using DbConfigurator.UI.Features.Recipients.Services;
using DbConfigurator.UI.Features.Regions.Services;
using DbConfigurator.UI.Startup;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace DbConfigurator.UI.Features.DistributionInformations.Services
{
    public class DistributionInformationService : GenericDataService<CreateDistributionInformationDto, UpdateDistributionInformationDto, DistributionInformation>, IDistributionInformationService
    {
        private readonly IRecipientService _recipientService;
        private readonly IRegionService _regionService;
        private readonly IAreaService _areaService;
        private readonly IBusinessUnitService _businessUnitService;
        private readonly ICountryService _countryService;

        public DistributionInformationService(
            IRecipientService recipientService,
            IRegionService regionService,
            IAreaService areaService,
            IBusinessUnitService businessUnitService,
            ICountryService countryService,
            IDbConfiguratorApiClient client,
            AutoMapperConfig autoMapper
            )
            : base(client, autoMapper, "DistributionInformation")
        {
            _recipientService = recipientService;
            _regionService = regionService;
            _areaService = areaService;
            _businessUnitService = businessUnitService;
            _countryService = countryService;
        }

        public async Task<IEnumerable<Priority>> GetAllPrioritiesAsync()
        {
            await Task.CompletedTask;
            return new List<Priority>();
        }

        public async Task<IEnumerable<Recipient>> GetAllRecipientsAsync()
        {
            var dtos = await _recipientService.GetAllAsync();
            var mapped = _mapper.Mapper.Map<IEnumerable<Recipient>>(dtos);
            return mapped;
        }

        public async Task<IEnumerable<Region>> GetAllRegionsAsync()
        {
            var regionsDto = await _regionService.GetAllAsync();
            var mapped = _mapper.Mapper.Map<IEnumerable<Region>>(regionsDto);
            return mapped;
        }

        public Task<IEnumerable<Region>> GetRegionsWithAsync(int areaId, int BusinessUnitId, int countryId)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<Area>> GetAllAreaFiltersForRegionAsync()
        {
            var regions = await _regionService.GetAllAsync();
            var AreasIdList = regions.Select(r => r.Area.Id).Distinct();
            var allAreas = await _areaService.GetAllAsync();
            var areas = allAreas.Where(a => AreasIdList.Contains(a.Id));

            return areas;
        }

        public async Task<IEnumerable<BusinessUnit>> GetAllBusinessUnitFiltersForRegionAsync(int? areaId = null)
        {
            var regions = await _regionService.GetAllAsync();
            if (areaId is not null)
            {
                regions = regions.Where(a => a.Area.Id == areaId);
            }
            var businessUnitsIdList = regions.Select(r => r.BusinessUnit.Id);
            var allBusinessUnits = await _businessUnitService.GetAllAsync();
            var businessUnits = allBusinessUnits.Where(b => businessUnitsIdList.Contains(b.Id));
            
            return businessUnits;
        }

        public async Task<IEnumerable<Country>> GetCountriyFiltersForRegionAsync(int? areaId = null, int? businessUnitId = null)
        {
            var regions = await _regionService.GetAllAsync();
            if (areaId is not null && businessUnitId is not null)
            {
                regions = regions.Where(a => 
                    a.Area.Id == areaId &&
                    a.BusinessUnit.Id == businessUnitId);
            }
            var countriesIdList = regions.Select(r => r.Country.Id);
            var allCountries = await _countryService.GetAllAsync();
            var countries = allCountries.Where(b => countriesIdList.Contains(b.Id));

            return countries;
        }

        //public DistributionInformationService(

        //    AutoMapperConfig autoMapper
        //    )
        //    : base(repository, autoMapper)
        //{
        //    _recipientRepository = recipientRepository;
        //    _priorityRepository = priorityRepository;
        //    _regionRepository = regionRepository;
        //    _BusinessUnitRepository = BusinessUnitRepository;
        //    _countryRepository = countryRepository;
        //}



        //public async Task<IEnumerable<Area>> GetUniqueAreasFromRegionAsync()
        //{
        //    var countries = await _regionRepository.GetUniqueAreasFromRegionAsync();

        //    return _autoMapper.Mapper.Map<IEnumerable<Area>>(countries);
        //}
        //public async Task<IEnumerable<BusinessUnit>> GetUniqueBusinessUnitsFromRegionAsync(int? areaId = null)
        //{
        //    IEnumerable<Region> regions = areaId is null ?
        //        await _regionRepository.GetAllAsync() :
        //        await _regionRepository.GetAllAsync(i => i.AreaId == areaId);

        //    var BusinessUnitsIdList = regions.Select(r => r.BusinessUnitId);
        //    var BusinessUnits = await _BusinessUnitRepository.GetAllAsync(b => BusinessUnitsIdList.Contains(b.Id));

        //    return _autoMapper.Mapper.Map<IEnumerable<BusinessUnit>>(BusinessUnits);
        //}
        //public async Task<IEnumerable<Country>> GetUniqueCountriesFromRegionAsync(int? areaId = null, int? BusinessUnitId = null)
        //{
        //    var regions = BusinessUnitId is null ?
        //        await _regionRepository.GetAllAsync() :
        //        await _regionRepository.GetAllAsync(i => i.AreaId == areaId && i.BusinessUnitId == BusinessUnitId);

        //    var countriesIdList = regions.Select(r => r.CountryId);
        //    var countries = await _countryRepository.GetAllAsync(c => countriesIdList.Contains(c.Id));

        //    return _autoMapper.Mapper.Map<IEnumerable<Country>>(countries);
        //}
        //public IEnumerable<Recipient> GetAllRecipients()
        //{
        //    var recipients = _recipientRepository.GetAll();

        //    return _autoMapper.Mapper.Map<IEnumerable<Recipient>>(recipients);
        //}
        //public async Task<IEnumerable<Recipient>> GetAllRecipientsAsync()
        //{
        //    var recipients = await _recipientRepository.GetAllAsync();

        //    return _autoMapper.Mapper.Map<IEnumerable<Recipient>>(recipients);
        //}
        //public async Task<IEnumerable<PriorityDto>> GetAllPrioritiesAsync()
        //{
        //    var priorities = await _priorityRepository.GetAllAsync();

        //    return _autoMapper.Mapper.Map<IEnumerable<PriorityDto>>(priorities);
        //}
        //public async Task<IEnumerable<Region>> GetRegionsWithAsync(int areaId, int BusinessUnitId, int countryId)
        //{
        //    var regions = await _regionRepository.GetRegionsWithAsync(areaId, BusinessUnitId, countryId);

        //    return _autoMapper.Mapper.Map<IEnumerable<Region>>(regions);
        //}
        //public async Task<IEnumerable<Region>> GetAllRegionsAsync()
        //{
        //    var regions = await _regionRepository.GetAllAsync();

        //    return _autoMapper.Mapper.Map<IEnumerable<Region>>(regions);
        //}
        //public override async Task<IEnumerable<DistributionInformation>> GetAllAsync()
        //{
        //    var distributionInformations = await _repository.GetAllAsync();

        //    return _autoMapper.Mapper.Map<IEnumerable<DistributionInformation>>(distributionInformations);
        //}
    }
}
