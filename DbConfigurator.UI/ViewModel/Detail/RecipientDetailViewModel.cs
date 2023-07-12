using DbConfigurator.Model.DTOs.Core;
using DbConfigurator.Model.Entities.Wrapper;
using DbConfigurator.UI.ViewModel.Base;

namespace DbConfigurator.UI.ViewModel.Detail
{
    public class RecipientDetailViewModel : DetailViewModelBase
    {
        public RecipientDetailViewModel()
        {
            RecipientDto recipientDto = new();
            Recipient = new(recipientDto);
            Recipient.FirstName = "";
            Recipient.LastName = "";
            Recipient.Email = "";

            Title = "Recipient";
            ViewWidth = 560;
            ViewHeight = 340;
        }

        public RecipientDtoWrapper Recipient { get; set; }
    }
}
