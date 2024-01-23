using DbConfigurator.Core.Models;
using DbConfigurator.UI.Base.Contracts;
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
