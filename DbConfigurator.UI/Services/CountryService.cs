using DbConfigurator.DataAccess.DTOs.AreaDto;
using DbConfigurator.Model.DTOs.Core;
using DbConfigurator.Model.Entities.Core;
using DbConfigurator.UI.Startup;

namespace DbConfigurator.UI.Services
{
    public class CountryService : GenericDataService<Area>, ICountryService
    {
        public CountryService(AutoMapperConfig autoMapper)
        //: base(repository, autoMapper)
        {
        }
    }
}
