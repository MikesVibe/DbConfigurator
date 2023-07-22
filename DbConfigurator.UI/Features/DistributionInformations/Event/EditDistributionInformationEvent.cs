using DbConfigurator.Model.DTOs.Core;
using DbConfigurator.UI.Contracts;
using Prism.Events;

namespace DbConfigurator.UI.Features.Areas.Event
{
    public class EditDistributionInformationEvent : PubSubEvent<EditDistributionInformationEventArgs>
    {
    }
    public class EditDistributionInformationEventArgs : IEventArgs<DistributionInformationDto>
    {
        public DistributionInformationDto Entity { get; set; } = default!;
    }
}
