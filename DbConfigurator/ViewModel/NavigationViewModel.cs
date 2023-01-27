using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using DbConfigurator.UI.ViewModel.Interfaces;

namespace DbConfigurator.UI.ViewModel
{
    public class NavigationViewModel : ViewModelBase, INavigationViewModel
    {
        public NavigationViewModel()
        {
            Recipients_ObservableCollection = new ObservableCollection<NavigationItemViewModel>();
        }






        public ObservableCollection<NavigationItemViewModel> Recipients_ObservableCollection { get; }

        public async Task LoadAsync()
        {
            await Task.Delay(0);

            Recipients_ObservableCollection.Add(new NavigationItemViewModel(0, "Distribution List"));
            Recipients_ObservableCollection.Add(new NavigationItemViewModel(1, "Recipients"));
            Recipients_ObservableCollection.Add(new NavigationItemViewModel(2, "Countries"));


        }
    }
}
