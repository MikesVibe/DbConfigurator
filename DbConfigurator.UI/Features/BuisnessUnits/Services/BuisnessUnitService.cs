using DbConfigurator.DataAccess;
using DbConfigurator.DataAccess.DTOs.BusinessUnitDtos;
using DbConfigurator.Model.Entities.Core;
using DbConfigurator.UI.Base;
using DbConfigurator.UI.Startup;

namespace DbConfigurator.UI.Features.BuisnessUnits.Services
{
    public class BusinessUnitService : GenericDataService<CreateBuisnessUnitDto, UpdateBuisnessUnitDto, BusinessUnit>, IBusinessUnitService
    {
        public BusinessUnitService(
            IDbConfiguratorApiClient client,
            AutoMapperConfig autoMapper) : base(client, autoMapper, "BusinessUnit")
        {
        }
        //public BusinessUnitService(BusinessUnitRepository repository, AutoMapperConfig autoMapper) : base(repository, autoMapper)
        //{
        //}
    }
}
