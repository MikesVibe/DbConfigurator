using DbConfigurator.Core.Models;
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
using DbConfigurator.UI.Features.Notification.Services;
using DbConfigurator.Core.Contracts;

namespace DbConfigurator.UI.Panels.NotificationPanel
{
    public class NotificationPanelViewModel : PanelViewModelBase, IMainPanelViewModel
    {
        private string _ticketNumber = string.Empty;
        private string _ticketSummary = string.Empty;
        private string _reportedBy = string.Empty;
        private string _openedBy = string.Empty;
        private string _description = string.Empty;
        private string _reportedTime = string.Empty;
        private string _openedTime = string.Empty;
        private string _gbus = string.Empty;

        private TicketType? _selectedTicketType;
        private Priority? _selectedPriority;
        private DateTime? _reportedDate;
        private DateTime? _openedDate;

        private bool _canCreateTicket = false;
        private bool _canCreateNotification = false;
        
        private readonly IStatusService _statusService;
        private readonly INotificationService _notificationService;
        private readonly EmailService _emailService;
        private readonly ISecuritySettings _securitySettings;
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

        public NotificationPanelViewModel(
            EmailService emailService,
            ISecuritySettings securitySettings,
            IEventAggregator eventAggregator,
            IStatusService statusService,
            INotificationService nofiticationService)
            : base(statusService)
        {
            GetFromOutlookCommand = new DelegateCommand(OnGetFromOutlookExecute);
            CreateTicketCommand = new DelegateCommand(OnCreateTicketExecute, () => CanCreateTicket);
            CreateNotificationCommand = new DelegateCommand(OnCreateNotificationExecute, () => CanCreateNotification);

            _emailService = emailService;
            _securitySettings = securitySettings;
            _eventAggregator = eventAggregator;
            _statusService = statusService;
            _notificationService = nofiticationService;
        }

        #region Public Properties
        public DelegateCommand GetFromOutlookCommand { get; }
        public DelegateCommand CreateTicketCommand { get; }
        public DelegateCommand CreateNotificationCommand { get; }
        public List<Priority> Priorities { get; set; } = new();
        public Priority? SelectedPriority
        {
            get => _selectedPriority;
            set
            {
                _selectedPriority = value;
                OnPropertyChanged();
            }
        }
        public List<TicketType> TicketTypes { get; } = new List<TicketType>() { TicketType.Incident, TicketType.Event };
        public TicketType? SelectedTicketType
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
        public bool CanCreateTicket
        {
            get => _canCreateTicket;
            set
            {
                _canCreateTicket = value;
                CreateTicketCommand.RaiseCanExecuteChanged();
            }
        }
        public bool CanCreateNotification
        {
            get => _canCreateNotification;
            set
            {
                _canCreateNotification = value;
                CreateNotificationCommand.RaiseCanExecuteChanged();
            }
        }
        #endregion Public Properties


        private void OnGetFromOutlookExecute()
        {
            var result = _emailService.GetEmailData();
            if (result.IsFailed)
            {
                MessageBox.Show(result.Errors.First().Message);
                return;
            }

            var emailData = result.Value;
            TicketSummary = emailData.Title;
            SelectedTicketType = TicketTypes.Single(t => t.ToString() == emailData.TicketType);
            ReportedBy = emailData.Requester;
            TicketDescription = emailData.Description;
            GBUs = emailData.GBU;
            ReportedDate = emailData.ReportedDate;
            ReportedTime = emailData.ReportedDate.ToShortTimeString();
            SelectPriorityByName(emailData.Priority);

            CanCreateTicket = true;
        }
        private void OnCreateTicketExecute()
        {
            TicketNumber = GenerateTicketNumber();
            OpenedDate = DateTime.Now;
            OpenedTime = OpenedDate.Value.ToShortTimeString();
            OpenedBy = _securitySettings.User;

            CanCreateNotification = true;
        }
        private string GenerateTicketNumber()
        {
            Random rdn = new();
            return $"INC{rdn.Next(1000000, 9999999)}";
        }
        private async void OnCreateNotificationExecute()
        {
            var result = await _notificationService.GetDistribiutionInfoWithMatchingRegionsAndMatchingPriorityAsync(GBUs, SelectedPriority!.Value);
            if (result.IsFailed)
            {
                MessageBox.Show(result.Errors.First().Message);
            }
            var distributionList = result.Value;

            _eventAggregator.GetEvent<SelectedNotificationDistributionList>()
                  .Publish(
                new SelectedNotificationDistributionListArgs
                {
                    DistributionInformationIds = distributionList.SelectedDistributionInformationIds,
                });

            var notificationData = CollectNotificationData();
            var emailCreatedSuccesfuly = _emailService.CreateReplayEmail(distributionList, notificationData);
            if (emailCreatedSuccesfuly.IsFailed)
            {
                MessageBox.Show(emailCreatedSuccesfuly.Errors.First().Message);
                return;
            }

            CanCreateTicket = false;
            CanCreateNotification = false;

            ResetAllFields();
        }
        private void ResetAllFields()
        {
            SelectedTicketType = null;
            SelectedPriority = null;
            ReportedBy = string.Empty;
            GBUs = string.Empty;
            TicketSummary = string.Empty;
            TicketDescription = string.Empty;
            ReportedDate = null;
            ReportedTime = string.Empty;
            TicketNumber = string.Empty;
            OpenedBy = string.Empty;
            OpenedDate = null;
            OpenedTime = string.Empty;
        }
        private NotificationData CollectNotificationData()
        {
            return new Model.NotificationData()
            {
                TicketNumber = TicketNumber,
                TicketDescription = TicketDescription,
                TicketSummary = TicketSummary,
                TicketType = SelectedTicketType.ToString()!,
                OpenedBy = OpenedBy,
                Priority = SelectedPriority!.ToString(),
                GBU = GBUs,
                OpenedDate = (DateTime)OpenedDate!,
                ReportedDate = (DateTime)ReportedDate!,
                ReportedBy = ReportedBy
            };
        }
        private void SelectPriorityByName(string name)
        {
            SelectedPriority = Priorities.Where(p => p.Name.ToLower() == name.ToLower()).Single();
        }

        protected override async Task LoadDataAsync()
        {
            Priorities = (await _notificationService.GetAllPriorities()).ToList();
            return;
        }

        public override Task RefreshAsync()
        {
            return Task.CompletedTask;
        }
    }
}
