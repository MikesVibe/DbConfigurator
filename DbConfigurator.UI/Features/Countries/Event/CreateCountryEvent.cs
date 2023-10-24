using DbConfigurator.Model.DTOs.Core;
using DbConfigurator.Model.Entities.Core;
using DbConfigurator.UI.Contracts;
using Prism.Events;

namespace DbConfigurator.UI.Event
{
    public class CreateCountryEvent : PubSubEvent<CreateCountryEventArgs>
    {
    }
    public class CreateCountryEventArgs : IEventArgs<Country>
    {
        public Country Entity { get; set; } = default!;
    }
}
