using DbConfigurator.Model.Entities.Core;
using DbConfigurator.UI.Contracts;
using Prism.Events;

namespace DbConfigurator.UI.Event
{
    public class CreateBusinessUnitEvent : PubSubEvent<CreateBusinessUnitEventArgs>
    {
    }
    public class CreateBusinessUnitEventArgs : IEventArgs<BusinessUnit>
    {
        public BusinessUnit Entity { get; set; } = default!;
    }
}
