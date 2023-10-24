using DbConfigurator.Model.DTOs.Core;
using DbConfigurator.Model.Entities.Core;
using DbConfigurator.UI.Startup;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DbConfigurator.UI.Services
{
    public class RegionService : GenericDataService<Region>, IRegionService
    {
        public RegionService(
            AutoMapperConfig autoMapper)
        : base(autoMapper)
        {

        }

        public Task<ICollection<Area>> GetAllAreasAsync()
        {
            throw new System.NotImplementedException();
        }

        public Task<ICollection<BusinessUnit>> GetAllBusinessUnitsAsync()
        {
            throw new System.NotImplementedException();
        }

        public Task<ICollection<Country>> GetAllCountriesAsync()
        {
            throw new System.NotImplementedException();
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
