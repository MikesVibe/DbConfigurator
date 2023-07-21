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
    public class CreateCountryEvent : PubSubEvent<CreateCountryEventArgs>
    {
    }
    public class CreateCountryEventArgs : IEventArgs<CountryDto>
    {
        public CountryDto Entity { get; set; } = default!;
    }
}
