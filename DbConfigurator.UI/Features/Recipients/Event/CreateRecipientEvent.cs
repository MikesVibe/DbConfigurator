using DbConfigurator.Model.DTOs.Core;
using DbConfigurator.UI.Contracts;
using Prism.Events;

namespace DbConfigurator.UI.Event
{
    public class CreateRecipientEvent : PubSubEvent<CreateRecipientEventArgs>
    {
    }
    public class CreateRecipientEventArgs : IEventArgs<RecipientDto>
    {
        public RecipientDto Entity { get; set; } = default!;
    }
}
