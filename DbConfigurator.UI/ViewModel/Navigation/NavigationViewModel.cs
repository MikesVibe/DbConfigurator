using DbConfigurator.UI.ViewModel.Base;
using DbConfigurator.UI.ViewModel.Interfaces;
using Prism.Events;
using System.Collections.ObjectModel;
using System.Threading.Tasks;

namespace DbConfigurator.UI.ViewModel.Navigation
{
    public class NavigationViewModel : ViewModelBase, INavigationViewModel
    {
        public NavigationViewModel(IEventAggregator eventAggregator)
        {
            NavigationItems_ObservableCollection = new ObservableCollection<NavigationItemViewModel>();

            _eventAggregator = eventAggregator;

        }


        public ObservableCollection<NavigationItemViewModel> NavigationItems_ObservableCollection { get; }


        public async Task LoadAsync()
        {
            await Task.Delay(0);

            NavigationItems_ObservableCollection.Add(
                new NavigationItemViewModel(0, "Distribution List", nameof(DistributionInformationTableViewModel), _eventAggregator));
            NavigationItems_ObservableCollection.Add(
                new NavigationItemViewModel(1, "Recipients", nameof(RecipientTableViewModel), _eventAggregator));
            NavigationItems_ObservableCollection.Add(
                new NavigationItemViewModel(2, "Regions", nameof(RegionTableViewModel), _eventAggregator));
            NavigationItems_ObservableCollection.Add(
                   new NavigationItemViewModel(3, "Create", nameof(CreationTableViewModel), _eventAggregator));

        }


        private IEventAggregator _eventAggregator;

    }
}
