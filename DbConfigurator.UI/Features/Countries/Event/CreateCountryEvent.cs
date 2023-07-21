using DbConfigurator.Model.DTOs.Core;
using Prism.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DbConfigurator.UI.Event
{
    public class CreateCountryEvent : PubSubEvent<CreateCountryEventArgs>
    {
    }
    public class CreateCountryEventArgs
    {
        public CountryDto Country { get; set; } = default!;
    }
}
