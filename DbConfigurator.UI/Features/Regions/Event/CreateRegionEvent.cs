using DbConfigurator.Model.DTOs.Core;
using DbConfigurator.Model.Entities.Core;
using DbConfigurator.UI.Contracts;
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
