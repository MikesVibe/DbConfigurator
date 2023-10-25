using DbConfigurator.DataAccess;
using DbConfigurator.Model.Entities.Core;
using DbConfigurator.UI.Startup;

namespace DbConfigurator.UI.Services
{
    public class CountryService : GenericDataService<Country>, ICountryService
    {
        public CountryService(
            IDbConfiguratorApiClient client,
            AutoMapperConfig autoMapper)
            : base(client, autoMapper, "Country")
        {
        }
    }
}
