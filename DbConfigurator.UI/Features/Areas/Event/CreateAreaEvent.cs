using DbConfigurator.Model.DTOs.Core;
using Prism.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DbConfigurator.UI.Event
{
    public class CreateAreaEvent : PubSubEvent<CreateAreaEventArgs>
    {
    }
    public class CreateAreaEventArgs
    {
        public AreaDto Area { get; set; } = default!;
    }
}
