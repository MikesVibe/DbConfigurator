using DbConfigurator.DataAccess;
using DbConfigurator.Model.Entities.Core;
using DbConfigurator.UI.Base;
using DbConfigurator.UI.Features.Areas.Services;
using DbConfigurator.UI.Features.BuisnessUnits.Services;
using DbConfigurator.UI.Features.Countries.Services;
using DbConfigurator.UI.Startup;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DbConfigurator.UI.Features.Regions.Services
{
    public class RegionService : GenericDataService<Region>, IRegionService
    {
        private readonly IAreaService _areaService;
        private readonly IBusinessUnitService _businessUnitService;
        private readonly ICountryService _countryService;
        private readonly AutoMapperConfig _autoMapper;

        public RegionService(
            IDbConfiguratorApiClient client,
            IAreaService areaService,
            IBusinessUnitService businessUnitService,
            ICountryService countryService,
            AutoMapperConfig autoMapper)
        : base(client, autoMapper, "Region")
        {
            _areaService = areaService;
            _businessUnitService = businessUnitService;
            _countryService = countryService;
            _autoMapper = autoMapper;
        }

        public async Task<IEnumerable<Area>> GetAllAreasAsync()
        {
            return await _areaService.GetAllAsync();
        }

        public async Task<IEnumerable<BusinessUnit>> GetAllBusinessUnitsAsync()
        {
            return await _businessUnitService.GetAllAsync();

        }

        public async Task<IEnumerable<Country>> GetAllCountriesAsync()
        {
            return await _countryService.GetAllAsync();
        }
        //public async Task<ICollection<Area>> GetAllAreasAsync()
        //{
        //    var areas = await _repository.GetAllAreasAsync();
        //    return _autoMapper.Mapper.Map<ICollection<Area>>(areas);
        //}
        //public async Task<ICollection<BusinessUnit>> GetAllBusinessUnitsAsync()
        //{
        //    var BusinessUnits = await _repository.GetAllBusinessUnitsAsync();

        //    return _autoMapper.Mapper.Map<ICollection<BusinessUnit>>(BusinessUnits);
        //}
        //public async Task<ICollection<Country>> GetAllCountriesAsync()
        //{
        //    var countries = await _repository.GetAllCountriesAsync();

        //    return _autoMapper.Mapper.Map<ICollection<Country>>(countries);
        //}
    }
}
