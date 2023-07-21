using DbConfigurator.Model.DTOs.Core;
using Prism.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DbConfigurator.UI.Event
{
    public class CreateBuisnessUnitEvent : PubSubEvent<CreateBuisnessUnitEventArgs>
    {
    }
    public class CreateBuisnessUnitEventArgs
    {
        public BuisnessUnitDto BuisnessUnit { get; set; } = default!;
    }
}
