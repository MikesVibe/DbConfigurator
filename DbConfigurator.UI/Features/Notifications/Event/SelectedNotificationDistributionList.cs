using DbConfigurator.Model.Entities.Core;
using DbConfigurator.UI.Base.Contracts;
using Prism.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DbConfigurator.UI.Features.Notifications.Event
{
    public class SelectedNotificationDistributionList : PubSubEvent<SelectedNotificationDistributionListArgs>
    {
    }
    public class SelectedNotificationDistributionListArgs
    {
        public List<int> DistributionInformationIds { get; set; } = new List<int>();
    }
}
