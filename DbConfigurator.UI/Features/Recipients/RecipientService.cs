using DbConfigurator.Model.DTOs.Core;
using DbConfigurator.Model.Entities.Core;
using DbConfigurator.UI.Services;
using DbConfigurator.UI.Services.Interfaces;
using DbConfigurator.UI.Startup;

namespace DbConfigurator.UI.Features.Recipients
{
    public class RecipientService : GenericDataService<Recipient, RecipientDto>, IRecipientService
    {
        public RecipientService(AutoMapperConfig autoMapper) 
            //: base(repository, autoMapper)
        {
        }
    }
}
