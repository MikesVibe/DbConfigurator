using DbConfigurator.Model.DTOs.Core;
using DbConfigurator.UI.Contracts;
using Prism.Events;

namespace DbConfigurator.UI.Features.Areas.Event
{
    public class EditBuisnessUnitEvent : PubSubEvent<EditBuisnessUnitEventArgs>
    {
    }
    public class EditBuisnessUnitEventArgs : IEventArgs<BuisnessUnitDto>
    {
        public BuisnessUnitDto Entity { get; set; } = default!;
    }
}
