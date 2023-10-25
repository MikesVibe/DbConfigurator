using DbConfigurator.DataAccess;
using DbConfigurator.Model.Entities.Core;
using DbConfigurator.UI.Base;
using DbConfigurator.UI.Startup;

namespace DbConfigurator.UI.Features.Recipients.Services
{
    public class RecipientService : GenericDataService<Recipient>, IRecipientService
    {
        public RecipientService(
            IDbConfiguratorApiClient client,

            AutoMapperConfig autoMapper)
        : base(client, autoMapper, "Recipient")
        {
        }
    }
}
