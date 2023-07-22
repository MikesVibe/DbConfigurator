using DbConfigurator.Model.DTOs.Core;
using DbConfigurator.UI.Contracts;
using Prism.Events;

namespace DbConfigurator.UI.Event
{
    public class CreateRegionEvent : PubSubEvent<CreateRegionEventArgs>
    {
    }
    public class CreateRegionEventArgs : IEventArgs<RegionDto>
    {
        public RegionDto Entity { get; set; } = default!;
    }
}
