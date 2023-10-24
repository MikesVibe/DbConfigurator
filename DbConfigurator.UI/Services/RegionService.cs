using DbConfigurator.DataAccess.DTOs.AreaDto;
using DbConfigurator.Model.DTOs.Core;
using DbConfigurator.Model.Entities.Core;
using DbConfigurator.UI.Startup;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DbConfigurator.UI.Services
{
    public class RegionService : GenericDataService<Area>, IRegionService
    {
        public RegionService(
            AutoMapperConfig autoMapper)
        //: base(regionRepository, autoMapper)
        {

        }

        public Task<ICollection<AreaDto>> GetAllAreasAsync()
        {
            throw new System.NotImplementedException();
        }

        public Task<ICollection<BusinessUnitDto>> GetAllBusinessUnitsAsync()
        {
            throw new System.NotImplementedException();
        }

        public Task<ICollection<CountryDto>> GetAllCountriesAsync()
        {
            throw new System.NotImplementedException();
        }
        //public async Task<ICollection<AreaDto>> GetAllAreasAsync()
        //{
        //    var areas = await _repository.GetAllAreasAsync();
        //    return _autoMapper.Mapper.Map<ICollection<AreaDto>>(areas);
        //}
        //public async Task<ICollection<BusinessUnitDto>> GetAllBusinessUnitsAsync()
        //{
        //    var BusinessUnits = await _repository.GetAllBusinessUnitsAsync();

        //    return _autoMapper.Mapper.Map<ICollection<BusinessUnitDto>>(BusinessUnits);
        //}
        //public async Task<ICollection<CountryDto>> GetAllCountriesAsync()
        //{
        //    var countries = await _repository.GetAllCountriesAsync();

        //    return _autoMapper.Mapper.Map<ICollection<CountryDto>>(countries);
        //}
    }
}
