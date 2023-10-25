using DbConfigurator.Model.DTOs.Core;
using DbConfigurator.Model.Entities.Core;
using DbConfigurator.UI.Startup;

namespace DbConfigurator.UI.Services
{
    public class BusinessUnitService : GenericDataService<BusinessUnit>, IBusinessUnitService
    {
        public BusinessUnitService(AutoMapperConfig autoMapper) : base(autoMapper, "BusinessUnit")
        {
        }
        //public BusinessUnitService(BusinessUnitRepository repository, AutoMapperConfig autoMapper) : base(repository, autoMapper)
        //{
        //}
    }
}
