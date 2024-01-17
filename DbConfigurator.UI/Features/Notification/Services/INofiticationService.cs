using DbConfigurator.Model.Entities.Core;
using DbConfigurator.UI.Panels.NotificationPanel;
using FluentResults;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DbConfigurator.UI.Features.Notification.Services
{
    public interface INofiticationService
    {
        Task<IEnumerable<Priority>> GetAllPriorities();
        Task<Result<IEnumerable<DistributionInformation>>> GetDistribiutionInfoWithMatchingRegionsAndMatchingPriorityAsync(string gbu, int priority);
        Task<IEnumerable<DistributionInformation>> GetMatchingDistributionInformationWithAny(Region exactlyMatchedRegion, NotificationPanelViewModel.MatchingRegion matchingRegion);
    }
}
