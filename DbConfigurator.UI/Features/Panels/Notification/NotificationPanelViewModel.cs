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

        public enum TicketType
        {
            Incident,
            Event
        }

        public NotificationPanelViewModel(IStatusService statusService) 
            : base(statusService)
        {
            GetFromOutlookCommand = new DelegateCommand(OnGetFromOutlookExecute);
            CreateTicketCommand = new DelegateCommand(OnCreateTicketExecute);
            CreateNotificationCommand = new DelegateCommand(OnCreateNotificationExecute);

            Priorities.Add(new Priority { Id = 1, Name = "P1" });
            Priorities.Add(new Priority { Id = 2, Name = "P2" });
            Priorities.Add(new Priority { Id = 3, Name = "P3" });
            Priorities.Add(new Priority { Id = 4, Name = "P4" });
            Priorities.Add(new Priority { Id = 99, Name = "ANY" });
            SelectPriorityByName("ANY");
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
        private void OnCreateNotificationExecute()
        {
            MessageBox.Show($"Successfully create notification");
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

        protected override Task LoadDataAsync()
        {
            return Task.CompletedTask;
        }

        public override Task RefreshAsync()
        {
            return Task.CompletedTask;
        }
    }
}
