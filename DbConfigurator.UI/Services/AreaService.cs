using DbConfigurator.DataAccess.Controllers;
using DbConfigurator.Model.DTOs.Core;
using DbConfigurator.Model.Entities.Core;
using DbConfigurator.UI.Startup;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DbConfigurator.UI.Services
{
    public class AreaService : GenericDataService<Area, AreaDto>, IAreaService
    {
        private readonly AreaController _areaController;

        public AreaService(AutoMapperConfig autoMapper, AreaController areaController)
        //: base(autoMapper)
        {
            _areaController = areaController;
        }

        //public override AreaDto GetById(int id)
        //{
        //    var entity = _areaController.GetById(id);
        //    return _autoMapper.Mapper.Map<AreaDto>(entity);
        //}
        //public override async Task<IEnumerable<AreaDto>> GetAllAsync()
        //{
        //    return _autoMapper.Mapper.Map<IEnumerable<AreaDto>>(await _areaController.GetAll());
        //}
    }
}
