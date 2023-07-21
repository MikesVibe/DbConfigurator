using DbConfigurator.Model.DTOs.Core;
using DbConfigurator.Model.Entities.Wrapper;
using DbConfigurator.UI.Services.Interfaces;
using DbConfigurator.UI.ViewModel.Base;

namespace DbConfigurator.UI.Features.Recipients
{
    public class RecipientDetailViewModel : DetailViewModelBase<IRecipientService, RecipientDto>
    {
        public RecipientDetailViewModel(IRecipientService recipientService) : base(recipientService)
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
