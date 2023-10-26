using DbConfigurator.Model.Entities.Core;
using DbConfigurator.UI.Base.Contracts;
using Prism.Events;

namespace DbConfigurator.UI.Features.Areas.Event
{
    public class EditRecipientEvent : PubSubEvent<EditRecipientEventArgs>
    {
    }
    public class EditRecipientEventArgs : IEventArgs<Recipient>
    {
        public Recipient Entity { get; set; } = default!;
    }
}
