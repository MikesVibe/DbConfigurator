using DbConfigurator.UI.Base.Contracts;
using DbConfigurator.UI.Features.Panels.Creation;
using DbConfigurator.UI.Features.Panels.DistributionInformation;
using DbConfigurator.UI.Features.Panels.Recipient;
using DbConfigurator.UI.Features.Panels.Region;
using DbConfigurator.UI.ViewModel.Base;
using Prism.Events;
using System.Collections.ObjectModel;
using System.Threading.Tasks;

namespace DbConfigurator.UI.Features.Panels.Navigation
{
    public class NavigationPanelViewModel : NotifyBase, INavigationPanelViewModel
    {
        private IEventAggregator _eventAggregator;

        public NavigationPanelViewModel(IEventAggregator eventAggregator)
        {
            NavigationItems_ObservableCollection = new ObservableCollection<NavigationItem>();

            _eventAggregator = eventAggregator;
        }

        public ObservableCollection<NavigationItem> NavigationItems_ObservableCollection { get; }

        public async Task LoadAsync()
        {
            await Task.Delay(0);

            NavigationItems_ObservableCollection.Add(
                new NavigationItem(0, "Distribution List", nameof(DistributionInformationPanelViewModel), _eventAggregator));
            NavigationItems_ObservableCollection.Add(
                new NavigationItem(1, "Recipients", nameof(RecipientPanelViewModel), _eventAggregator));
            NavigationItems_ObservableCollection.Add(
                new NavigationItem(2, "Regions", nameof(RegionPanelViewModel), _eventAggregator));
            NavigationItems_ObservableCollection.Add(
                   new NavigationItem(3, "Create", nameof(CreationPanelViewModel), _eventAggregator));
        }
    }
}
