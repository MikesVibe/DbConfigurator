using DbConfigurator.Model.DTOs.Core;
using DbConfigurator.UI.Contracts;
using Prism.Events;

namespace DbConfigurator.UI.Event
{
    public class CreateBusinessUnitEvent : PubSubEvent<CreateBusinessUnitEventArgs>
    {
    }
    public class CreateBusinessUnitEventArgs : IEventArgs<BusinessUnitDto>
    {
        public BusinessUnitDto Entity { get; set; } = default!;
    }
}
