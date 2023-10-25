using DbConfigurator.DataAccess;
using DbConfigurator.Model.DTOs.Core;
using DbConfigurator.Model.Entities.Core;
using DbConfigurator.UI.Startup;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DbConfigurator.UI.Services
{
    public class DistributionInformationService : GenericDataService<DistributionInformation>, IDistributionInformationService
    {
        public DistributionInformationService(
            IDbConfiguratorApiClient client,

            AutoMapperConfig autoMapper
            )
            : base(client, autoMapper, "DistributionInformation")
        {
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
        public Task<IEnumerable<PriorityDto>> GetAllPrioritiesAsync()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Recipient> GetAllRecipients()
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Recipient>> GetAllRecipientsAsync()
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Region>> GetAllRegionsAsync()
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Region>> GetRegionsWithAsync(int areaId, int BusinessUnitId, int countryId)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Area>> GetUniqueAreasFromRegionAsync()
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<BusinessUnit>> GetUniqueBusinessUnitsFromRegionAsync(int? areaId = null)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Country>> GetUniqueCountriesFromRegionAsync(int? areaId = null, int? BusinessUnitId = null)
        {
            throw new NotImplementedException();
        }
    }
}
