using DbConfigurator.Model.Entities.Core;
using DbConfigurator.UI.Base.Contracts;
using Prism.Events;

namespace DbConfigurator.UI.Features.Areas.Event
{
    public class EditRegionEvent : PubSubEvent<EditRegionEventArgs>
    {
    }
    public class EditRegionEventArgs : IEventArgs<Region>
    {
        public Region Entity { get; set; } = default!;
    }
}
