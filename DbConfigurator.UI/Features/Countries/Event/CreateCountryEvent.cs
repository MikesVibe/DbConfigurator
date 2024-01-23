using DbConfigurator.Core.Models;
using DbConfigurator.UI.Base.Contracts;
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
