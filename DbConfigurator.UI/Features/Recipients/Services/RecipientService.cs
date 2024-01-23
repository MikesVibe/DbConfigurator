using DbConfigurator.DataAccess;
using DbConfigurator.DataAccess.DTOs.RecipientDtos;
using DbConfigurator.Core.Models;
using DbConfigurator.UI.Base;
using DbConfigurator.UI.Base.Contracts;
using DbConfigurator.UI.Startup;

namespace DbConfigurator.UI.Features.Recipients.Services
{
    public class RecipientService : GenericDataService<CreateRecipientDto, UpdateRecipientDto, Recipient>, IRecipientService
    {
        public RecipientService(
            IDbConfiguratorApiClient client,
            IStatusService statusService,
            AutoMapperConfig autoMapper)
        : base(client, statusService, autoMapper, "Recipient")
        {
        }
    }
}
