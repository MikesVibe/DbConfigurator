using DbConfigurator.Model.DTOs.Core;
using DbConfigurator.UI.Contracts;
using Prism.Events;

namespace DbConfigurator.UI.Features.Areas.Event
{
    public class EditCountryEvent : PubSubEvent<EditCountryEventArgs>
    {
    }
    public class EditCountryEventArgs : IEventArgs<CountryDto>
    {
        public CountryDto Entity { get; set; } = default!;
    }
}
