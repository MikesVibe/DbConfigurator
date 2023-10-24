using DbConfigurator.Model.DTOs.Core;
using DbConfigurator.Model.Entities.Core;
using DbConfigurator.UI.Startup;

namespace DbConfigurator.UI.Services
{
    public class RecipientService : GenericDataService<Recipient>, IRecipientService
    {
        public RecipientService(AutoMapperConfig autoMapper)
        : base(autoMapper)
        {
        }
    }
}
