using DbConfigurator.UI.ViewModel.Base;
using DbConfigurator.UI.ViewModel.Interfaces;
using DbConfigurator.UI.ViewModel.Navigation;
using Prism.Events;
using System.Collections.ObjectModel;
using System.Threading.Tasks;

namespace DbConfigurator.UI.ViewModel.Panel
{
    public class NavigationPanelViewModel : ViewModelBase, INavigationPanelViewModel
    {
        private IEventAggregator _eventAggregator;
        
        public NavigationPanelViewModel(IEventAggregator eventAggregator)
        {
            NavigationItems_ObservableCollection = new ObservableCollection<NavigationItemViewModel>();

            _eventAggregator = eventAggregator;
        }

        public ObservableCollection<NavigationItemViewModel> NavigationItems_ObservableCollection { get; }

        public async Task LoadAsync()
        {
            await Task.Delay(0);

            NavigationItems_ObservableCollection.Add(
                new NavigationItemViewModel(0, "Distribution List", nameof(DistributionInformationPanelViewModel), _eventAggregator));
            NavigationItems_ObservableCollection.Add(
                new NavigationItemViewModel(1, "Recipients", nameof(RecipientPanelViewModel), _eventAggregator));
            NavigationItems_ObservableCollection.Add(
                new NavigationItemViewModel(2, "Regions", nameof(RegionPanelViewModel), _eventAggregator));
            NavigationItems_ObservableCollection.Add(
                   new NavigationItemViewModel(3, "Create", nameof(CreationPanelViewModel), _eventAggregator));
        }
    }
}
