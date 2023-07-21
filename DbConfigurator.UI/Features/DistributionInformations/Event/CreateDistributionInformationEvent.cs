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
    public class CreateDistributionInformationEvent : PubSubEvent<CreateDistributionInformationEventArgs>
    {
    }
    public class CreateDistributionInformationEventArgs : IEventArgs<DistributionInformationDto>
    {
        public DistributionInformationDto Entity { get; set; } = default!;
    }
}
