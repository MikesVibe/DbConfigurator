using DbConfigurator.Model.Entities.Core;
using Prism.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows;
using DbConfigurator.UI.ViewModel.Base;
using DbConfigurator.UI.Base.Contracts;
using FluentResults;
using DbConfigurator.UI.Features.DistributionInformations.Services;
using DbConfigurator.UI.Features.Areas.Services;
using DbConfigurator.UI.Features.BuisnessUnits.Services;
using DbConfigurator.UI.Features.Countries.Services;
using DbConfigurator.UI.Features.Priorities.Services;
using DbConfigurator.UI.Features.Regions.Services;
using DbConfigurator.DataAccess.DTOs;
using DbConfigurator.UI.Features.Areas.Event;
using Prism.Events;
using DbConfigurator.UI.Features.Notifications.Event;
using DbConfigurator.UI.Services;
using DbConfigurator.Authentication;
using DbConfigurator.Model;

namespace DbConfigurator.UI.Panels.NotificationPanel
{
    public class NotificationPanelViewModel : PanelViewModelBase, IMainPanelViewModel
    {
        private string _ticketNumber;
        private string _ticketSummary;
        private string _reportedBy;
        private string _openedBy;
        private string _description;
        private TicketType _selectedTicketType;
        private DateTime? _reportedDate;
        private DateTime? _openedDate;
        private string _reportedTime;
        private string _openedTime;
        private Priority _selectedPriority;
        private string _gbus;
        private readonly IStatusService _statusService;
        private readonly IDistributionInformationService _distributionInformationService;
        private readonly IAreaService _areaService;
        private readonly IBusinessUnitService _businessUnitService;
        private readonly ICountryService _countryService;
        private readonly IPriorityService _priorityService;
        private readonly IRegionService _regionService;
        private readonly EmailService _emailService;
        private readonly SecuritySettings _securitySettings;
        private readonly IEventAggregator _eventAggregator;

        public enum TicketType
        {
            Incident,
            Event
        }
        public enum MatchingRegion
        {
            Country,
            CountryAndBuisnessUnit
        }
        public enum RegionField
        {
            Area = 0,
            BusinessUnit = 1,
            CountryName = 2,
            CountryCode = 3
        }

        public NotificationPanelViewModel(IStatusService statusService,
            IDistributionInformationService distributionInformationService,
            IAreaService areaService,
            IBusinessUnitService businessUnitService,
            ICountryService countryService,
            IPriorityService priorityService,
            IRegionService regionService,
            EmailService emailService,
            SecuritySettings securitySettings,
            IEventAggregator eventAggregator)
            : base(statusService)
        {
            GetFromOutlookCommand = new DelegateCommand(OnGetFromOutlookExecute);
            CreateTicketCommand = new DelegateCommand(OnCreateTicketExecute);
            CreateNotificationCommand = new DelegateCommand(OnCreateNotificationExecute);

            _statusService = statusService;
            _distributionInformationService = distributionInformationService;
            _areaService = areaService;
            _businessUnitService = businessUnitService;
            _countryService = countryService;
            _priorityService = priorityService;
            _regionService = regionService;
            _emailService = emailService;
            _securitySettings = securitySettings;
            _eventAggregator = eventAggregator;
        }




        #region Public Properties
        public ICommand GetFromOutlookCommand { get; }
        public ICommand CreateTicketCommand { get; }
        public ICommand CreateNotificationCommand { get; }
        public List<Priority> Priorities { get; set; } = new();
        public Priority SelectedPriority
        {
            get => _selectedPriority;
            set
            {
                _selectedPriority = value;
                OnPropertyChanged();
            }
        }
        public List<TicketType> TicketTypes { get; } = new List<TicketType>() { TicketType.Incident, TicketType.Event };
        public TicketType SelectedTicketType
        {
            get => _selectedTicketType;
            set
            {
                _selectedTicketType = value;
                OnPropertyChanged();
            }
        }
        public string TicketNumber
        {
            get => _ticketNumber;
            set
            {
                _ticketNumber = value;
                OnPropertyChanged();
            }
        }
        public string TicketSummary
        {
            get => _ticketSummary;
            set
            {
                _ticketSummary = value;
                OnPropertyChanged();
            }
        }
        public string GBUs
        {
            get => _gbus;
            set
            {
                _gbus = value;
                OnPropertyChanged();
            }
        }
        public string ReportedBy
        {
            get => _reportedBy;
            set
            {
                _reportedBy = value;
                OnPropertyChanged();
            }
        }
        public string OpenedBy
        {
            get => _openedBy;
            set
            {
                _openedBy = value;
                OnPropertyChanged();
            }
        }
        public string TicketDescription
        {
            get => _description;
            set
            {
                _description = value;
                OnPropertyChanged();
            }
        }
        public DateTime? ReportedDate
        {
            get { return _reportedDate; }
            set
            {
                _reportedDate = value;
                OnPropertyChanged();
            }
        }
        public string ReportedTime
        {
            get { return _reportedTime; }
            set
            {
                _reportedTime = value;
                OnPropertyChanged();
            }
        }
        public DateTime? OpenedDate
        {
            get { return _openedDate; }
            set
            {
                _openedDate = value;
                OnPropertyChanged();
            }
        }
        public string OpenedTime
        {
            get { return _openedTime; }
            set
            {
                _openedTime = value;
                OnPropertyChanged();
            }
        }
        #endregion Public Properties


        private void OnGetFromOutlookExecute()
        {
            var result = _emailService.GetEmailData();

            if (result.IsSuccess)
            {
                var emailData = result.Value;
                TicketSummary = emailData.Title;
                SelectedTicketType = TicketTypes.Single(t => t.ToString() == emailData.TicketType);
                ReportedBy = emailData.Requester;
                TicketDescription = emailData.Description;
                GBUs = emailData.GBU;
                ReportedDate = emailData.ReportedDate;
                ReportedTime = emailData.ReportedDate.ToShortTimeString();
                SelectPriorityByName(emailData.Priority);
            }
            else
            {
                MessageBox.Show($"Data couldn't be retrived from email.");
            }
        }
        private void OnCreateTicketExecute()
        {
            TicketNumber = GenerateTicketNumber();
            OpenedDate = DateTime.Now;
            OpenedTime = OpenedDate.Value.ToShortTimeString();
            OpenedBy = _securitySettings.User;

            //MessageBox.Show($"Successfully created ticket with number: {TicketNumber}");
        }
        private async void OnCreateNotificationExecute()
        {
            var result = await GetDistributionListBySingleName();
            if (result.IsFailed)
            {
                MessageBox.Show($"Couldn't create notification");
                return;
            }

            try
            {
                var notificationData = GetNotificationData();
                var emailCreatedSuccesfuly = _emailService.CreateReplayEmail(result.Value, notificationData);
                if (!emailCreatedSuccesfuly)
                {
                    MessageBox.Show($"Couldn't create notification");
                    return;
                }
            }
            catch (ArgumentNullException ex)
            {
                MessageBox.Show($"Couldn't create notification");
                return;
            }
        }

        private NotificationData GetNotificationData()
        {
            if (OpenedDate is null || ReportedDate is null)
            {
                throw new ArgumentNullException();
            }

            return new Model.NotificationData()
            {
                TicketNumber = TicketNumber,
                TicketDescription = TicketDescription,
                TicketSummary = TicketSummary,
                TicketType = SelectedTicketType.ToString(),
                OpenedBy = OpenedBy,
                Priority = SelectedPriority.ToString(),
                GBU = GBUs,
                OpenedDate = (DateTime)OpenedDate,
                ReportedDate = (DateTime)ReportedDate,
                ReportedBy = ReportedBy
            };
        }

        private string GenerateTicketNumber()
        {
            Random rdn = new();
            return $"INC{rdn.Next(1000000, 9999999)}";
        }
        private void SelectPriorityByName(string name)
        {
            SelectedPriority = Priorities.Where(p => p.Name.ToLower() == name.ToLower()).Single();
        }

        protected override async Task LoadDataAsync()
        {
            var priorities = await _priorityService.GetAllAsync();
            Priorities = priorities.Where(p => p.Name.ToUpper() != "ANY").ToList();
            SelectPriorityByName("P4");

            return;
        }

        public override Task RefreshAsync()
        {
            return Task.CompletedTask;
        }

        public async Task<Result<DistributionList>> GetDistributionListBySingleName()
        {
            var toReturn = new DistributionList();
            var result = await GetDistribiutionInfoWithMatchingRegions(GBUs);
            if (result.IsFailed)
            {
                return Result.Fail("Fail");
            }

            var matchingDisInfoByPriority = result.Value.Where(d =>
                d.Priority.Value >= SelectedPriority.Value);

            var idsList = matchingDisInfoByPriority.Select(d => d.Id).ToList();
            _eventAggregator.GetEvent<SelectedNotificationDistributionList>()
                  .Publish(
                new SelectedNotificationDistributionListArgs
                {
                    DistributionInformationIds = idsList,
                });

            foreach (var disfInfo in matchingDisInfoByPriority)
            {
                toReturn.RecipientsTo.AddRange(disfInfo.RecipientsTo);
                toReturn.RecipientsCc.AddRange(disfInfo.RecipientsCc);
            }

            return toReturn;
        }



        private async Task<Result<IEnumerable<DistributionInformation>>> GetDistribiutionInfoWithMatchingRegions(string gbu)
        {
            var disInfoToReturn = new List<DistributionInformation>();

            var distributionInformation = await _distributionInformationService.GetAllAsync();
            var allAreas = await _areaService.GetAllAsync();
            var allBuisnessUnits = await _businessUnitService.GetAllAsync();
            var allCountries = await _countryService.GetAllAsync();
            var allRegions = await _regionService.GetAllAsync();

            if (
                allAreas == null ||
                allBuisnessUnits == null ||
                allCountries == null ||
                distributionInformation == null
                )
            {
                return Result.Fail("fail");
            }

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
                return Result.Fail("Fail");
            }





            return disInfoToReturn;
        }

        private async Task<IEnumerable<DistributionInformation>> GetMatchingDistributionInformationWithAny(Region exactlyMatchedRegion, MatchingRegion matchingRegion)
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
