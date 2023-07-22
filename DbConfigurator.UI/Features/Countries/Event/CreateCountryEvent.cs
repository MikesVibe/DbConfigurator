using DbConfigurator.Model.DTOs.Core;
using DbConfigurator.UI.Contracts;
using Prism.Events;

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
