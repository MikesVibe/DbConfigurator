using DbConfigurator.DataAccess;
using DbConfigurator.Model.Entities.Core;
using DbConfigurator.UI.Startup;

namespace DbConfigurator.UI.Services
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
