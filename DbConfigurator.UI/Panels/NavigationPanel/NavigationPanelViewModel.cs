using DbConfigurator.UI.Base.Contracts;
using DbConfigurator.UI.Panels.DistributionInformationPanel;
using DbConfigurator.UI.Panels.NotificationPanel;
using DbConfigurator.UI.Panels.RecipientPanel;
using DbConfigurator.UI.Panels.RegionPanel;
using DbConfigurator.UI.Panels.CreationPanel;
using DbConfigurator.UI.ViewModel.Base;
using Prism.Commands;
using Prism.Events;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using DbConfigurator.Authentication;
using static DbConfigurator.Authentication.Role;
using System;
using DbConfigurator.Core;
using DbConfigurator.Core.Contracts;

namespace DbConfigurator.UI.Panels.NavigationPanel
{
    public class NavigationPanelViewModel : NotifyBase, INavigationPanelViewModel
    {
        private IEventAggregator _eventAggregator;
        private readonly ISecuritySettings _securitySettings;
        private bool _isAuthorizedToShowDatabaseConfigurationBorder = false;
        private bool _isAuthorizedToShowNotificationBorder = false;

        public NavigationPanelViewModel(IEventAggregator eventAggregator, ISecuritySettings securitySettings)
        {
            _eventAggregator = eventAggregator;
            _securitySettings = securitySettings;
            _securitySettings.UserLoggedIn += OnUserLoginExecute;

        }

        private void OnUserLoginExecute(object sender, UserLoggedInEventArgs e)
        {
            if (_securitySettings.UserRole == UserRole.Admin || _securitySettings.UserRole == UserRole.SecurityAnalyst)
            {
                ShouldShowNotificationBorder = true;
            }
            if (_securitySettings.UserRole == UserRole.Admin || _securitySettings.UserRole == UserRole.DatabaseManager || _securitySettings.UserRole == UserRole.SecurityAnalyst)
            {
                ShouldShowDatabaseConfigurationBorder = true;
            }
        }

        public bool ShouldShowDatabaseConfigurationBorder
        {
            get { return _isAuthorizedToShowDatabaseConfigurationBorder; }
            set
            {
                _isAuthorizedToShowDatabaseConfigurationBorder = value;
                OnPropertyChanged();
            }
        }
        public bool ShouldShowNotificationBorder
        {
            get { return _isAuthorizedToShowNotificationBorder; }
            set
            {
                _isAuthorizedToShowNotificationBorder = value;
                OnPropertyChanged();
            }
        }
        public ObservableCollection<NavigationItem> DbConfigurationNavigationItems_ObservableCollection { get; } = new ObservableCollection<NavigationItem>();
        public ObservableCollection<NavigationItem> NotificationNavigationItems_ObservableCollection { get; } = new ObservableCollection<NavigationItem>();

        public async Task LoadAsync()
        {
            await Task.Delay(0);

            DbConfigurationNavigationItems_ObservableCollection.Add(
                new NavigationItem(0, "Distribution List", nameof(DistributionInformationPanelViewModel), _eventAggregator));
            DbConfigurationNavigationItems_ObservableCollection.Add(
                new NavigationItem(1, "Recipients", nameof(RecipientPanelViewModel), _eventAggregator));
            DbConfigurationNavigationItems_ObservableCollection.Add(
                new NavigationItem(2, "Regions", nameof(RegionPanelViewModel), _eventAggregator));
            DbConfigurationNavigationItems_ObservableCollection.Add(
                new NavigationItem(3, "Region Units", nameof(CreationPanelViewModel), _eventAggregator));

            NotificationNavigationItems_ObservableCollection.Add(
                new NavigationItem(0, "Create", nameof(NotificationPanelViewModel), _eventAggregator));

            DbConfigurationNavigationItems_ObservableCollection.First().OpenTabelViewCommand.Execute(new object());
        }

    }
}
