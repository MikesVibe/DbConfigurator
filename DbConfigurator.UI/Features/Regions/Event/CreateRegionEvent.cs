using DbConfigurator.Core.Models;
using DbConfigurator.UI.Base.Contracts;
using Prism.Events;

namespace DbConfigurator.UI.Event
{
    public class CreateRegionEvent : PubSubEvent<CreateRegionEventArgs>
    {
    }
    public class CreateRegionEventArgs : IEventArgs<Region>
    {
        public Region Entity { get; set; } = default!;
    }
}
