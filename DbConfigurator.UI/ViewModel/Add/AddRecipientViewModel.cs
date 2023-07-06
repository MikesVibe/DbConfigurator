using DbConfigurator.Model.DTOs.Core;
using DbConfigurator.Model.Entities.Wrapper;
using DbConfigurator.UI.ViewModel.Base;

namespace DbConfigurator.UI.ViewModel.Add
{
    public class AddRecipientViewModel : EditingViewModelBase
    {
        public AddRecipientViewModel()
        {
            RecipientDto recipientDto = new();
            Recipient = new(recipientDto);
            Recipient.FirstName = "";
            Recipient.LastName = "";
            Recipient.Email = "";
        }

        public RecipientDtoWrapper Recipient { get; set; }
    }
}
