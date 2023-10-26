using DbConfigurator.Model.Entities.Core;
using DbConfigurator.UI.Base.Contracts;
using Prism.Events;

namespace DbConfigurator.UI.Features.Areas.Event
{
    public class EditCountryEvent : PubSubEvent<EditCountryEventArgs>
    {
    }
    public class EditCountryEventArgs : IEventArgs<Country>
    {
        public Country Entity { get; set; } = default!;
    }
}
