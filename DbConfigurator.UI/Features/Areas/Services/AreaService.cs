using DbConfigurator.DataAccess;
using DbConfigurator.DataAccess.DTOs.AreaDtos;
using DbConfigurator.Model.Entities.Core;
using DbConfigurator.UI.Base;
using DbConfigurator.UI.Startup;

namespace DbConfigurator.UI.Features.Areas.Services
{
    public class AreaService : GenericDataService<CreateAreaDto, UpdateAreaDto, Area>, IAreaService
    {
        public AreaService(
            IDbConfiguratorApiClient client,
            AutoMapperConfig autoMapper)
        : base(client, autoMapper, "Area")
        {
        }
    }
}
