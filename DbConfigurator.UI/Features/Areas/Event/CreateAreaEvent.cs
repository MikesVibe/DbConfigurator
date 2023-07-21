using DbConfigurator.Model.DTOs.Core;
using DbConfigurator.UI.Contracts;
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
    public class CreateAreaEventArgs : IEventArgs<AreaDto>
    {
        public AreaDto Entity { get; set; } = default!;
    }
}
