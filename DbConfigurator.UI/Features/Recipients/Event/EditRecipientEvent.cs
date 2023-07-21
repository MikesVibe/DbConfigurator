using DbConfigurator.Model.DTOs.Core;
using DbConfigurator.UI.Contracts;
using Prism.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DbConfigurator.UI.Features.Areas.Event
{
    public class EditRecipientEvent : PubSubEvent<EditRecipientEventArgs>
    {
    }
    public class EditRecipientEventArgs : IEventArgs<RecipientDto>
    {
        public RecipientDto Entity { get; set; } = default!;
    }
}
