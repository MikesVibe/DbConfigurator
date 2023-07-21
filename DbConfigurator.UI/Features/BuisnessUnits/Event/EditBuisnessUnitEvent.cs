using DbConfigurator.Model.DTOs.Core;
using Prism.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DbConfigurator.UI.Features.Areas.Event
{
    public class EditBuisnessUnitEvent : PubSubEvent<EditBuisnessUnitEventArgs>
    {
    }
    public class EditBuisnessUnitEventArgs
    {
        public BuisnessUnitDto BuisnessUnit { get; set; } = default!;
    }
}
