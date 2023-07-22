using DbConfigurator.Model.DTOs.Core;
using DbConfigurator.UI.Contracts;
using Prism.Events;

namespace DbConfigurator.UI.Event
{
    public class CreateDistributionInformationEvent : PubSubEvent<CreateDistributionInformationEventArgs>
    {
    }
    public class CreateDistributionInformationEventArgs : IEventArgs<DistributionInformationDto>
    {
        public DistributionInformationDto Entity { get; set; } = default!;
    }
}
