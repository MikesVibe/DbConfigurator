using DbConfigurator.Model.Entities.Core;
using DbConfigurator.UI.Contracts;
using Prism.Events;

namespace DbConfigurator.UI.Features.Areas.Event
{
    public class EditAreaEvent : PubSubEvent<EditAreaEventArgs>
    {
    }
    public class EditAreaEventArgs : IEventArgs<Area>
    {
        public Area Entity { get; set; } = default!;
    }
}
