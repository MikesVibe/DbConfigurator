using DbConfigurator.Model.DTOs.Core;
using DbConfigurator.UI.Contracts;
using Prism.Events;

namespace DbConfigurator.UI.Features.Areas.Event
{
    public class EditAreaEvent : PubSubEvent<EditAreaEventArgs>
    {
    }
    public class EditAreaEventArgs : IEventArgs<AreaDto>
    {
        public AreaDto Entity { get; set; } = default!;
    }
}
