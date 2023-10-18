using DbConfigurator.Model.DTOs.Core;
using DbConfigurator.UI.Contracts;
using Prism.Events;

namespace DbConfigurator.UI.Features.Areas.Event
{
    public class EditBusinessUnitEvent : PubSubEvent<EditBusinessUnitEventArgs>
    {
    }
    public class EditBusinessUnitEventArgs : IEventArgs<BusinessUnitDto>
    {
        public BusinessUnitDto Entity { get; set; } = default!;
    }
}
