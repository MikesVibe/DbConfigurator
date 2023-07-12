using DbConfigurator.DataAccess.Repositories;
using DbConfigurator.Model.DTOs.Core;
using DbConfigurator.Model.Entities.Core;
using DbConfigurator.UI.Services.Interfaces;
using DbConfigurator.UI.Startup;

namespace DbConfigurator.UI.Services
{
    public class CountryService : GenericDataService<Country, CountryDto, CountryRepository>, ICountryService
    {
        public CountryService(CountryRepository repository, AutoMapperConfig autoMapper) : base(repository, autoMapper)
        {
        }
    }
}
