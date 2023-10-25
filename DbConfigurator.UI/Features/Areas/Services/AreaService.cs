using DbConfigurator.DataAccess;
using DbConfigurator.Model.Entities.Core;
using DbConfigurator.UI.Base;
using DbConfigurator.UI.Startup;

namespace DbConfigurator.UI.Features.Areas.Services
{
    public class AreaService : GenericDataService<Area>, IAreaService
    {
        public AreaService(
            IDbConfiguratorApiClient client,
            AutoMapperConfig autoMapper)
        : base(client, autoMapper, "Area")
        {
        }
    }
}
