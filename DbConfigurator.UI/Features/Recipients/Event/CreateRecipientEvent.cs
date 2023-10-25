using DbConfigurator.Model.Entities.Core;
using DbConfigurator.UI.Contracts;
using Prism.Events;

namespace DbConfigurator.UI.Event
{
    public class CreateRecipientEvent : PubSubEvent<CreateRecipientEventArgs>
    {
    }
    public class CreateRecipientEventArgs : IEventArgs<Recipient>
    {
        public Recipient Entity { get; set; } = default!;
    }
}
