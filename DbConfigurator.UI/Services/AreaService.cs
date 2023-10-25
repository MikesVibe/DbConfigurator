using DbConfigurator.DataAccess.Controllers;
using DbConfigurator.Model.Contracts;
using DbConfigurator.Model.DTOs.Core;
using DbConfigurator.Model.Entities.Core;
using DbConfigurator.UI.Startup;
using System.Collections.Generic;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace DbConfigurator.UI.Services
{
    public class AreaService : GenericDataService<Area>, IAreaService
    {
        private readonly AreaController _areaController;

        public AreaService(AutoMapperConfig autoMapper)
        : base(autoMapper)
        {
        }

        //public override Area GetById(int id)
        //{
        //    var entity = _areaController.GetById(id);
        //    return _autoMapper.Mapper.Map<Area>(entity);
        //}
        //public override async Task<IEnumerable<Area>> GetAllAsync()
        //{
        //    return _autoMapper.Mapper.Map<IEnumerable<Area>>(await _areaController.GetAll());
        //}
        public override async Task<IEnumerable<Area>> GetAllAsync()
        {
            IEnumerable<Area> toReturn;
            try
            {
                var dto = await _httpClient.GetFromJsonAsync<IEnumerable<Area>>($"area/all");
                toReturn = _mapper.Mapper.Map<IEnumerable<Area>>(dto);
            }
            catch
            {
                return new List<Area>();
            }

            return toReturn;
        }
    }
}
