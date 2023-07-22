using DbConfigurator.Model.DTOs.Core;
using DbConfigurator.UI.Contracts;
using Prism.Events;

namespace DbConfigurator.UI.Event
{
    public class CreateBuisnessUnitEvent : PubSubEvent<CreateBuisnessUnitEventArgs>
    {
    }
    public class CreateBuisnessUnitEventArgs : IEventArgs<BuisnessUnitDto>
    {
        public BuisnessUnitDto Entity { get; set; } = default!;
    }
}
