using DbConfigurator.DataAccess.Repository;
using DbConfigurator.Model.DTOs.Core;
using DbConfigurator.Model.Entities.Core;
using DbConfigurator.UI.Services;
using DbConfigurator.UI.Services.Interfaces;
using DbConfigurator.UI.Startup;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DbConfigurator.UI.Features.Regions
{
    public class RegionService : GenericDataService<Region, RegionDto, RegionRepository>, IRegionService
    {
        public RegionService(
            RegionRepository regionRepository,
            AutoMapperConfig autoMapper) : base(regionRepository, autoMapper)
        {

        }
        public async Task<ICollection<AreaDto>> GetAllAreasAsync()
        {
            var areas = await _repository.GetAllAreasAsync();
            return _autoMapper.Mapper.Map<ICollection<AreaDto>>(areas);
        }
        public async Task<ICollection<BusinessUnitDto>> GetAllBusinessUnitsAsync()
        {
            var BusinessUnits = await _repository.GetAllBusinessUnitsAsync();

            return _autoMapper.Mapper.Map<ICollection<BusinessUnitDto>>(BusinessUnits);
        }
        public async Task<ICollection<CountryDto>> GetAllCountriesAsync()
        {
            var countries = await _repository.GetAllCountriesAsync();

            return _autoMapper.Mapper.Map<ICollection<CountryDto>>(countries);
        }
    }
}
