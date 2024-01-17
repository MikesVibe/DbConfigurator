using DbConfigurator.UI.Base.Contracts;
using DbConfigurator.UI.Features.Areas.Services;
using DbConfigurator.UI.Features.BuisnessUnits.Services;
using DbConfigurator.UI.Features.Countries.Services;
using DbConfigurator.UI.Features.DistributionInformations.Services;
using DbConfigurator.UI.Features.Priorities.Services;
using DbConfigurator.UI.Features.Regions.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DbConfigurator.UI.Features.Notification.Services
{
    public class NotificationService : INofiticationService
    {
        private readonly IStatusService _statusService;
        private readonly IDistributionInformationService _distributionInformationService;
        private readonly IAreaService _areaService;
        private readonly IBusinessUnitService _businessUnitService;
        private readonly ICountryService _countryService;
        private readonly IPriorityService _priorityService;
        private readonly IRegionService _regionService;

        public NotificationService()
        {
        }

        public NotificationService(
            IStatusService statusService,
            IDistributionInformationService distributionInformationService,
            IAreaService areaService,
            IBusinessUnitService businessUnitService,
            ICountryService countryService,
            IPriorityService priorityService,
            IRegionService regionService
            )
        {
            _statusService = statusService;
            _distributionInformationService = distributionInformationService;
            _areaService = areaService;
            _businessUnitService = businessUnitService;
            _countryService = countryService;
            _priorityService = priorityService;
            _regionService = regionService;
        }
    }
}
