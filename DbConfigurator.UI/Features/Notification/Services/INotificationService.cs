using DbConfigurator.Core.Models;
using DbConfigurator.UI.Panels.NotificationPanel;
using FluentResults;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DbConfigurator.UI.Features.Notification.Services
{
    public interface INotificationService
    {
        Task<IEnumerable<Priority>> GetAllPriorities();
        Task<Result<DistributionList>> GetDistribiutionInfoWithMatchingRegionsAndMatchingPriorityAsync(string gbu, int priority);
        Task<IEnumerable<DistributionInformation>> GetMatchingDistributionInformationWithAny(Region exactlyMatchedRegion, NotificationPanelViewModel.MatchingRegion matchingRegion);
    }
}
