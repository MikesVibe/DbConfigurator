using DbConfigurator.Model.DTOs.Core;
using DbConfigurator.Model.Entities.Core;
using DbConfigurator.Model.Entities.Wrapper;
using DbConfigurator.UI.Services;
using DbConfigurator.UI.ViewModel.Base;
using Prism.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
