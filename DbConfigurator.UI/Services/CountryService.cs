using DbConfigurator.Model.DTOs.Core;
using DbConfigurator.Model.Entities.Core;
using DbConfigurator.UI.Startup;

namespace DbConfigurator.UI.Services
{
    public class CountryService : GenericDataService<Country, CountryDto>, ICountryService
    {
        public CountryService(AutoMapperConfig autoMapper)
        //: base(repository, autoMapper)
        {
        }
    }
}
