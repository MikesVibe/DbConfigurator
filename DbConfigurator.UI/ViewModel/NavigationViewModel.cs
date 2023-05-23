using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using DbConfigurator.UI.ViewModel.Interfaces;
using Prism.Events;

namespace DbConfigurator.UI.ViewModel
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
                new NavigationItemViewModel(2, "Regions", nameof(RegionCreatorViewModel), _eventAggregator));
            NavigationItems_ObservableCollection.Add(
              new NavigationItemViewModel(3, "Yas", nameof(CountryTableViewModel), _eventAggregator));
        }


        private IEventAggregator _eventAggregator;

    }
}
