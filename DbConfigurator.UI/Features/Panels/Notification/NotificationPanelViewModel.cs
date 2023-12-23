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

namespace DbConfigurator.UI.Features.Panels.Notification
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
        private readonly IEventAggregator _eventAggregator;

        public enum TicketType
        {
            Incident,
            Event
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
        public string Description
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
            TicketSummary = "Testing ticket summary";
            SelectedTicketType = TicketTypes.Single(t => t == TicketType.Event);
            ReportedBy = "Andrzej Kowalski";
            Description = "Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua." +
                "\r\nCommodo ullamcorper a lacus vestibulum sed arcu non. Eu augue ut lectus arcu bibendum at varius vel pharetra." +
                "\r\nPenatibus et magnis dis parturient. Gravida neque convallis a cras semper auctor neque vitae tempus." +
                "\r\nUrna neque viverra justo nec ultrices. Nibh nisl condimentum id venenatis a condimentum vitae sapien pellentesque." +
                "\r\nEget felis eget nunc lobortis mattis. Pellentesque elit eget gravida cum sociis natoque penatibus et magnis. Viverra orci sagittis eu volutpat." +
                "\r\nPlatea dictumst quisque sagittis purus sit amet volutpat consequat mauris. Mauris vitae ultricies leo integer malesuada nunc.";
            GBUs = "Poland";
            Random rnd = new Random();
            ReportedDate = DateTime.Now.AddMinutes(rnd.Next(-7, -1));
            ReportedTime = ReportedDate.Value.ToShortTimeString();
            SelectPriorityByName("P2");


            //MessageBox.Show($"Successfully retrieved data");
        }
        private void OnCreateTicketExecute()
        {
            TicketNumber = GenerateTicketNumber();
            OpenedDate = DateTime.Now;
            OpenedTime = OpenedDate.Value.ToShortTimeString();
            OpenedBy = "Mikołaj Mrukowski";

            //MessageBox.Show($"Successfully created ticket with number: {TicketNumber}");
        }
        private async void OnCreateNotificationExecute()
        {
            var result = await GetDistributionListBySingleName();
            if(result.IsSuccess)
            {
                var body = CreateNotificationBody(result.Value);


                MessageBox.Show($"Successfully create notification\n\n{body}");
            }
            else
            {
                MessageBox.Show($"Couldn't create notification :(");
            }
        }

        private string CreateNotificationBody(DistributionList distributionList)
        {
            string toEmails = string.Join(", ", distributionList.RecipientsTo.Select(r => r.Email));
            string ccEmails = string.Join(", ", distributionList.RecipientsCc.Select(r => r.Email));

            string toReturn = $"Ticket Type: {SelectedTicketType.ToString()}\n" +
                $"Ticket Number: {TicketNumber}\n" +
                $"Ticket Summary: {TicketSummary}\n" +
                $"Priority: {SelectedPriority}\n" +
                $"GBUs: {GBUs}\n" +
                $"Recipients To: {toEmails}\n" +
                $"Recipients Cc: {ccEmails}\n";

            return toReturn;
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
            if(result.IsFailed)
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

        

        private async Task<Result<IEnumerable<Model.Entities.Core.DistributionInformation>>> GetDistribiutionInfoWithMatchingRegions(string gbu)
        {
            var disInfoToReturn = new List<Model.Entities.Core.DistributionInformation>();

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

            Model.Entities.Core.Region exactlyMatchedRegion;

            if (matchingByArea.Count() == 1)
            {
                var matchedArea = matchingByArea.Single();
                exactlyMatchedRegion = allRegions.Where(r => r.Area.Id == matchedArea.Id).First();

                var partiallyMatchedRegion = allRegions.Where(r =>
                    r.Area.Id == exactlyMatchedRegion.Area.Id &&
                    r.BusinessUnit.Name.ToUpper() == "ANY" &&
                    r.Country.CountryName.ToUpper() == "ANY").Single();

                disInfoToReturn.AddRange(distributionInformation.Where(d => d.Region.Id == partiallyMatchedRegion.Id));
            }
            else if (matchingByBusinessUnit.Count() == 1)
            {
                var matchedBuisnessUnit = matchingByBusinessUnit.Single();
                exactlyMatchedRegion = allRegions.Where(r => r.BusinessUnit.Id == matchedBuisnessUnit.Id).First();


                var partiallyMatchedRegion = allRegions.Where(r =>
                    r.Area.Id == exactlyMatchedRegion.Area.Id &&
                    r.BusinessUnit.Name.ToUpper() == "ANY" &&
                    r.Country.CountryName.ToUpper() == "ANY").Single();

                disInfoToReturn.AddRange(distributionInformation.Where(d => d.Region.Id == partiallyMatchedRegion.Id));


                partiallyMatchedRegion = allRegions.Where(r =>
                    r.Area.Id == exactlyMatchedRegion.Area.Id &&
                    r.BusinessUnit.Id == exactlyMatchedRegion.BusinessUnit.Id &&
                    r.Country.CountryName.ToUpper() == "ANY").Single();

                disInfoToReturn.AddRange(distributionInformation.Where(d => d.Region.Id == partiallyMatchedRegion.Id));
            }
            else if (matchingByCountryName.Count() == 1 || matchingByCountryCode.Count() == 1)
            {
                var matchedCountry = matchingByCountryName.SingleOrDefault() ?? matchingByCountryCode.Single();
                exactlyMatchedRegion = allRegions.Where(r => r.Country.Id == matchedCountry.Id).Single();

                var partiallyMatchedRegion = allRegions.Where(r =>
                    r.Area.Id == exactlyMatchedRegion.Area.Id &&
                    r.BusinessUnit.Name.ToUpper() == "ANY" &&
                    r.Country.CountryName.ToUpper() == "ANY").Single();

                disInfoToReturn.AddRange(distributionInformation.Where(d => d.Region.Id == partiallyMatchedRegion.Id));


                partiallyMatchedRegion = allRegions.Where(r =>
                    r.Area.Id == exactlyMatchedRegion.Area.Id &&
                    r.BusinessUnit.Id == exactlyMatchedRegion.BusinessUnit.Id &&
                    r.Country.CountryName.ToUpper() == "ANY").Single();

                disInfoToReturn.AddRange(distributionInformation.Where(d => d.Region.Id == partiallyMatchedRegion.Id));

                //Add distributionInformation that contains matching Region
                disInfoToReturn.AddRange(distributionInformation.Where(d => d.Region.Id == exactlyMatchedRegion.Id).ToList());
            }
            else
            {
                return Result.Fail("Fail");
            }





            return disInfoToReturn;
        }
    }
}
