using DbConfigurator.DataAccess;
using DbConfigurator.DataAccess.DTOs.AreaDtos;
using DbConfigurator.Core.Models;
using DbConfigurator.UI.Base;
using DbConfigurator.UI.Base.Contracts;
using DbConfigurator.UI.Startup;

namespace DbConfigurator.UI.Features.Areas.Services
{
    public class AreaService : GenericDataService<CreateAreaDto, UpdateAreaDto, Area>, IAreaService
    {
        public AreaService(
            IDbConfiguratorApiClient client,
            IStatusService statusService,
            AutoMapperConfig autoMapper)
        : base(client, statusService, autoMapper, "Area")
        {
        }
    }
}
