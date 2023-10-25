using DbConfigurator.DataAccess.Controllers;
using DbConfigurator.Model.Entities.Core;
using DbConfigurator.UI.Startup;

namespace DbConfigurator.UI.Services
{
    public class AreaService : GenericDataService<Area>, IAreaService
    {
        private readonly AreaController _areaController;

        public AreaService(AutoMapperConfig autoMapper)
        : base(autoMapper, "Area")
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

    }
}
