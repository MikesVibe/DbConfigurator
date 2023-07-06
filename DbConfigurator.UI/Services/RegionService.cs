using DbConfigurator.DataAccess.Repository;
using DbConfigurator.Model.DTOs.Core;
using DbConfigurator.Model.Entities.Core;
using DbConfigurator.UI.Services.Interfaces;
using DbConfigurator.UI.Startup;

namespace DbConfigurator.UI.Services
{
    public class RegionService : GenericDataService<Region, RegionDto, RegionRepository>, IRegionService
    {
        public RegionService(
            RegionRepository regionRepository,
            AutoMapperConfig autoMapper) : base(regionRepository, autoMapper)
        {
        }
    }
}
