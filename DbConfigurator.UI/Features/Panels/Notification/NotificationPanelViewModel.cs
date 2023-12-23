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
            IPriorityService priorityService)
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


            MessageBox.Show($"Successfully retrieved data");
        }
        private void OnCreateTicketExecute()
        {
            TicketNumber = GenerateTicketNumber();
            OpenedDate = DateTime.Now;
            OpenedTime = OpenedDate.Value.ToShortTimeString();
            OpenedBy = "Mikołaj Mrukowski";

            MessageBox.Show($"Successfully created ticket with number: {TicketNumber}");
        }
        private async void OnCreateNotificationExecute()
        {
            var result = await GetDistributionListBySingleName();
            if(result.IsSuccess)
            {
                MessageBox.Show($"Successfully create notification");
            }
            else
            {
                MessageBox.Show($"Couldn't create notification :(");
            }
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
            Priorities = priorities.ToList();
            SelectPriorityByName("ANY");

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
            //var matchingDisInfo = GetDisInfoWithMatchingRegionField(matchinRegionField);

            var matchingDisInfoByPriority = result.Value.Where(d =>
                d.Priority.Value >= SelectedPriority.Value);

            foreach (var disfInfo in matchingDisInfoByPriority)
            {
                toReturn.RecipientsTo.AddRange(disfInfo.RecipientsTo);
                toReturn.RecipientsCc.AddRange(disfInfo.RecipientsCc);
            }

            return toReturn;
        }

        

        private async Task<Result<IEnumerable<Model.Entities.Core.DistributionInformation>>> GetDistribiutionInfoWithMatchingRegions(string gbu)
        {
            var distributionInformation = await _distributionInformationService.GetAllAsync();
            var allAreas = await _areaService.GetAllAsync();
            var allBuisnessUnits = await _businessUnitService.GetAllAsync();
            var allCountries = await _countryService.GetAllAsync();
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

            if (matchingByArea.Count() == 1)
            {
                return distributionInformation.Where(d => d.Region.Area.Id == matchingByArea.Single().Id).ToList();
            }
            if (matchingByBusinessUnit.Count() == 1)
            {
                return distributionInformation.Where(d => d.Region.BusinessUnit.Id == matchingByBusinessUnit.Single().Id).ToList();
            }
            if (matchingByCountryName.Count() == 1 || matchingByCountryCode.Count() == 1)
            {
                var matchedCountry = matchingByCountryName.SingleOrDefault() ?? matchingByCountryCode.Single();
                //Find first item in distributionInformation that contains matching Country
                var disInfoWithMatchingCountry = distributionInformation.Where(d => d.Region.Country.Id == matchedCountry.Id).ToList();
                var firstExactCountryMatch = disInfoWithMatchingCountry.FirstOrDefault();
                if (firstExactCountryMatch is null)
                    return Result.Fail("Fail");

                disInfoWithMatchingCountry.AddRange(distributionInformation.Where(d =>
                d.Region.BusinessUnit.Id == firstExactCountryMatch.Region.BusinessUnit.Id &&
                d.Region.Country.CountryName == "ANY"));

                return disInfoWithMatchingCountry;
            }


            return Result.Fail("Fail");
        }
    }
}
