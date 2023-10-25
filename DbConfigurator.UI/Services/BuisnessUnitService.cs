using DbConfigurator.DataAccess;
using DbConfigurator.Model.Entities.Core;
using DbConfigurator.UI.Startup;

namespace DbConfigurator.UI.Services
{
    public class BusinessUnitService : GenericDataService<BusinessUnit>, IBusinessUnitService
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
