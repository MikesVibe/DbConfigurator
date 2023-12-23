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

namespace DbConfigurator.UI.Panels.NavigationPanel
{
    public class NavigationPanelViewModel : NotifyBase, INavigationPanelViewModel
    {
        private IEventAggregator _eventAggregator;

        private bool _isAuthorizedToShowDatabaseConfigurationBorder = true;
        private bool _isAuthorizedToShowNotificationBorder = true;

        public NavigationPanelViewModel(IEventAggregator eventAggregator)
        {
            _eventAggregator = eventAggregator;
        }
        public bool ShouldShowDatabaseConfigurationBorder
        {
            get { return _isAuthorizedToShowDatabaseConfigurationBorder; }
            set
            {
                if (_isAuthorizedToShowDatabaseConfigurationBorder != value)
                {
                    _isAuthorizedToShowDatabaseConfigurationBorder = value;
                }
            }
        }
        public bool ShouldShowNotificationBorder
        {
            get { return _isAuthorizedToShowNotificationBorder; }
            set
            {
                if (_isAuthorizedToShowNotificationBorder != value)
                {
                    _isAuthorizedToShowNotificationBorder = value;
                }
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
                new NavigationItem(3, "Other", nameof(CreationPanelViewModel), _eventAggregator));

            NotificationNavigationItems_ObservableCollection.Add(
                new NavigationItem(0, "Create", nameof(NotificationPanelViewModel), _eventAggregator));

            DbConfigurationNavigationItems_ObservableCollection.First().OpenTabelViewCommand.Execute(new object());
        }

    }
}
