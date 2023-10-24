using DbConfigurator.Model.DTOs.Core;
using DbConfigurator.Model.Entities.Core;
using DbConfigurator.UI.Contracts;
using Prism.Events;

namespace DbConfigurator.UI.Event
{
    public class CreateAreaEvent : PubSubEvent<CreateAreaEventArgs>
    {
    }
    public class CreateAreaEventArgs : IEventArgs<Area>
    {
        public Area Entity { get; set; } = default!;
    }
}
