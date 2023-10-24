using DbConfigurator.Model.DTOs.Core;
using DbConfigurator.Model.Entities.Core;
using DbConfigurator.UI.Services;
using DbConfigurator.UI.Services.Interfaces;
using DbConfigurator.UI.Startup;

namespace DbConfigurator.UI.Features.Countries
{
    public class CountryService : GenericDataService<Country, CountryDto>, ICountryService
    {
        public CountryService(AutoMapperConfig autoMapper)
            //: base(repository, autoMapper)
        {
        }
    }
}
