using DbConfigurator.Model.DTOs.Core;
using DbConfigurator.UI.Contracts;
using Prism.Events;

namespace DbConfigurator.UI.Event
{
    public class CreateAreaEvent : PubSubEvent<CreateAreaEventArgs>
    {
    }
    public class CreateAreaEventArgs : IEventArgs<AreaDto>
    {
        public AreaDto Entity { get; set; } = default!;
    }
}
