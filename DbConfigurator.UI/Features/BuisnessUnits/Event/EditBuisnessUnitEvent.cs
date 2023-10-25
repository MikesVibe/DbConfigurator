using DbConfigurator.Model.Entities.Core;
using DbConfigurator.UI.Contracts;
using Prism.Events;

namespace DbConfigurator.UI.Features.Areas.Event
{
    public class EditBusinessUnitEvent : PubSubEvent<EditBusinessUnitEventArgs>
    {
    }
    public class EditBusinessUnitEventArgs : IEventArgs<BusinessUnit>
    {
        public BusinessUnit Entity { get; set; } = default!;
    }
}
