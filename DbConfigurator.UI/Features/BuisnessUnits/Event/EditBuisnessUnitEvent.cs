using DbConfigurator.Core.Models;
using DbConfigurator.UI.Base.Contracts;
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
