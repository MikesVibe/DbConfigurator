﻿using DbConfigurator.Core.Models;
using DbConfigurator.UI.Base.Contracts;
using Prism.Events;

namespace DbConfigurator.UI.Features.Areas.Event
{
    public class EditDistributionInformationEvent : PubSubEvent<EditDistributionInformationEventArgs>
    {
    }
    public class EditDistributionInformationEventArgs : IEventArgs<DistributionInformation>
    {
        public DistributionInformation Entity { get; set; } = default!;
    }
}
