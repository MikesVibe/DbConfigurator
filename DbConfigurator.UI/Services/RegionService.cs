using DbConfigurator.DataAccess.Repository;
using DbConfigurator.Model.DTOs.Core;
using DbConfigurator.Model.Entities.Core;
using DbConfigurator.UI.Services.Interfaces;
using DbConfigurator.UI.Startup;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DbConfigurator.UI.Services
{
    public class RegionService : GenericDataService<Region, RegionDto, RegionRepository>, IRegionService
    {
        public RegionService(
            RegionRepository regionRepository,
            AutoMapperConfig autoMapper) : base(regionRepository, autoMapper)
        {
        }

        public async Task<IEnumerable<AreaDto>> GetAreasAsync()
        {
            var areas = await _repository.GetAreasAsync();

            return _autoMapper.Mapper.Map<IEnumerable<AreaDto>>(areas);
        }
        public async Task<IEnumerable<BuisnessUnitDto>> GetBuisnessUnitsAsync()
        {
            var buisnessUnits = await _repository.GetBuisnessUnitsAsync();

            return _autoMapper.Mapper.Map<IEnumerable<BuisnessUnitDto>>(buisnessUnits);
        }
        public async Task<IEnumerable<CountryDto>> GetCountriesAsync()
        {
            var countries = await _repository.GetCountriesAsync();

            return _autoMapper.Mapper.Map<IEnumerable<CountryDto>>(countries);
        }
    }
}
