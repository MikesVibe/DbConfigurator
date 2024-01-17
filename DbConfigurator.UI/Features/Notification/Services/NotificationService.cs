using DbConfigurator.Model.Entities.Core;
using DbConfigurator.UI.Base.Contracts;
using DbConfigurator.UI.Features.Areas.Services;
using DbConfigurator.UI.Features.BuisnessUnits.Services;
using DbConfigurator.UI.Features.Countries.Services;
using DbConfigurator.UI.Features.DistributionInformations.Services;
using DbConfigurator.UI.Features.Priorities.Services;
using DbConfigurator.UI.Features.Regions.Services;
using FluentResults;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static DbConfigurator.UI.Panels.NotificationPanel.NotificationPanelViewModel;

namespace DbConfigurator.UI.Features.Notification.Services
{
    public class NotificationService : INofiticationService
    {
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
            _distributionInformationService = distributionInformationService;
            _areaService = areaService;
            _businessUnitService = businessUnitService;
            _countryService = countryService;
            _priorityService = priorityService;
            _regionService = regionService;
        }

        public async Task<IEnumerable<Priority>> GetAllPriorities()
        {
            var priorities = await _priorityService.GetAllAsync();
            return priorities.Where(p => p.Name.ToUpper() != "ANY").ToList();
        }

        public async Task<Result<IEnumerable<DistributionInformation>>> GetDistribiutionInfoWithMatchingRegionsAndMatchingPriorityAsync(string gbu, int priority)
        {
            var disInfoToReturn = new List<DistributionInformation>();

            var distributionInformation = await _distributionInformationService.GetAllAsync();
            var allAreas = await _areaService.GetAllAsync();
            var allBuisnessUnits = await _businessUnitService.GetAllAsync();
            var allCountries = await _countryService.GetAllAsync();
            var allRegions = await _regionService.GetAllAsync();

            //if (
            //    allAreas == null ||
            //    allBuisnessUnits == null ||
            //    allCountries == null ||
            //    distributionInformation == null
            //    )
            //{
            //    return Result.Fail("Could not retrive data API");
            //}

            var matchingByArea = allAreas.Where(d =>
                d.Name == gbu).ToList();
            var matchingByBusinessUnit = allBuisnessUnits.Where(d =>
                d.Name == gbu).ToList();
            var matchingByCountryName = allCountries.Where(d =>
                d.CountryName == gbu).ToList();
            var matchingByCountryCode = allCountries.Where(d =>
                d.CountryCode == gbu).ToList();

            //Add distribution information that matches ANY-thing
            disInfoToReturn.AddRange(distributionInformation.Where(d =>
                d.Region.Area.Name.ToUpper() == "ANY" &&
                d.Region.BusinessUnit.Name.ToUpper() == "ANY" &&
                d.Region.Country.CountryName.ToUpper() == "ANY"));

            Region exactlyMatchedRegion;

            if (matchingByArea.Count() == 1)
            {
                var matchedArea = matchingByArea.Single();
                exactlyMatchedRegion = allRegions.Where(r => r.Area.Id == matchedArea.Id).First();

                var disInfo = await GetMatchingDistributionInformationWithAny(exactlyMatchedRegion, MatchingRegion.CountryAndBuisnessUnit);
                disInfoToReturn.AddRange(disInfo);
            }
            else if (matchingByBusinessUnit.Count() == 1)
            {
                var matchedBuisnessUnit = matchingByBusinessUnit.Single();
                exactlyMatchedRegion = allRegions.Where(r => r.BusinessUnit.Id == matchedBuisnessUnit.Id).First();

                var disInfo = await GetMatchingDistributionInformationWithAny(exactlyMatchedRegion, MatchingRegion.CountryAndBuisnessUnit);
                disInfoToReturn.AddRange(disInfo);
                disInfo = await GetMatchingDistributionInformationWithAny(exactlyMatchedRegion, MatchingRegion.Country);
                disInfoToReturn.AddRange(disInfo);
            }
            else if (matchingByCountryName.Count() == 1 || matchingByCountryCode.Count() == 1)
            {
                var matchedCountry = matchingByCountryName.SingleOrDefault() ?? matchingByCountryCode.Single();
                exactlyMatchedRegion = allRegions.Where(r => r.Country.Id == matchedCountry.Id).Single();

                var disInfo = await GetMatchingDistributionInformationWithAny(exactlyMatchedRegion, MatchingRegion.CountryAndBuisnessUnit);
                disInfoToReturn.AddRange(disInfo);
                disInfo = await GetMatchingDistributionInformationWithAny(exactlyMatchedRegion, MatchingRegion.Country);
                disInfoToReturn.AddRange(disInfo);

                //Add distributionInformation that contains matching Region
                disInfoToReturn.AddRange(distributionInformation.Where(d => d.Region.Id == exactlyMatchedRegion.Id).ToList());
            }
            else
            {
                return Result.Fail($"Could not find GBU with specified name: {gbu}");
            }

            var matchingDisInfoByPriority = disInfoToReturn.Where(d =>
                d.Priority.Value >= priority);

            return disInfoToReturn;
        }

        public async Task<IEnumerable<DistributionInformation>> GetMatchingDistributionInformationWithAny(Region exactlyMatchedRegion, MatchingRegion matchingRegion)
        {
            var distributionInformation = await _distributionInformationService.GetAllAsync();
            var allRegions = await _regionService.GetAllAsync();

            if (matchingRegion == MatchingRegion.CountryAndBuisnessUnit)
            {
                var partiallyMatchedRegion = allRegions.Where(r =>
                    r.Area.Id == exactlyMatchedRegion.Area.Id &&
                    r.BusinessUnit.Name.ToUpper() == "ANY" &&
                    r.Country.CountryName.ToUpper() == "ANY").Single();

                return distributionInformation.Where(d => d.Region.Id == partiallyMatchedRegion.Id);
            }
            else if (matchingRegion == MatchingRegion.Country)
            {
                var partiallyMatchedRegion = allRegions.Where(r =>
                    r.Area.Id == exactlyMatchedRegion.Area.Id &&
                    r.BusinessUnit.Id == exactlyMatchedRegion.BusinessUnit.Id &&
                    r.Country.CountryName.ToUpper() == "ANY").Single();

                return distributionInformation.Where(d => d.Region.Id == partiallyMatchedRegion.Id);
            }
            else
            {
                return new List<DistributionInformation>();
            }
        }
    }
}
