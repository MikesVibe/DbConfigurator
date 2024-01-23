using DbConfigurator.Core.Models;
using DbConfigurator.UI.Base.Contracts;
using Prism.Events;

namespace DbConfigurator.UI.Event
{
    public class CreateDistributionInformationEvent : PubSubEvent<CreateDistributionInformationEventArgs>
    {
    }
    public class CreateDistributionInformationEventArgs : IEventArgs<DistributionInformation>
    {
        public DistributionInformation Entity { get; set; } = default!;
    }
}
