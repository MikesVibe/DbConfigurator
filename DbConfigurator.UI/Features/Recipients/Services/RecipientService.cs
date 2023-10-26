using DbConfigurator.DataAccess;
using DbConfigurator.DataAccess.DTOs.RecipientDtos;
using DbConfigurator.Model.Entities.Core;
using DbConfigurator.UI.Base;
using DbConfigurator.UI.Startup;

namespace DbConfigurator.UI.Features.Recipients.Services
{
    public class RecipientService : GenericDataService<CreateRecipientDto, UpdateRecipientDto, Recipient>, IRecipientService
    {
        public RecipientService(
            IDbConfiguratorApiClient client,

            AutoMapperConfig autoMapper)
        : base(client, autoMapper, "Recipient")
        {
        }
    }
}
