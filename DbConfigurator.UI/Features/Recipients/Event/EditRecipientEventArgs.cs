using DbConfigurator.Model.DTOs.Core;
using Prism.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DbConfigurator.UI.Features.Areas.Event
{
    public class EditRecipientEventArgs : PubSubEvent<EditRecipientEventArgsArgs>
    {
    }
    public class EditRecipientEventArgsArgs
    {
        public RecipientDto Recipient { get; set; } = default!;
    }
}
