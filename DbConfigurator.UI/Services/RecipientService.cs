using DbConfigurator.DataAccess.DTOs.AreaDto;
using DbConfigurator.Model.DTOs.Core;
using DbConfigurator.Model.Entities.Core;
using DbConfigurator.UI.Startup;

namespace DbConfigurator.UI.Services
{
    public class RecipientService : GenericDataService<CreateAreaDto, UpdateAreaDto, Area>, IRecipientService
    {
        public RecipientService(AutoMapperConfig autoMapper)
        //: base(repository, autoMapper)
        {
        }
    }
}
