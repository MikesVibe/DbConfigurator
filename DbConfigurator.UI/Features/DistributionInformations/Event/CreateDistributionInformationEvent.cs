using DbConfigurator.Model.Entities.Core;
using DbConfigurator.UI.Contracts;
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
