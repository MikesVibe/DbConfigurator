using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DbConfigurator.UI.ViewModel.Interfaces;

namespace DbConfigurator.UI.ViewModel
{
    public class MainViewModel : ViewModelBase
    {
        public MainViewModel()
        {
            //DetailViewModels = new ObservableCollection<IDetailViewModel>();
            SelectedDetailViewModel = new DetailsViewModel();
            NavigationViewModel = new NavigationViewModel();
        }

        public ObservableCollection<IDetailViewModel> DetailViewModels { get; }

        public INavigationViewModel NavigationViewModel
        {
            get { return _navigationViewModel; }
            set { _navigationViewModel = value; }
        }
        public IDetailViewModel SelectedDetailViewModel
        {
            get { return _selectedDetailViewModel; }
            set { _selectedDetailViewModel = value; }
        }
        public async Task LoadAsync()
        {
            await NavigationViewModel.LoadAsync();
        }

        private IDetailViewModel _selectedDetailViewModel;
        private INavigationViewModel _navigationViewModel;


    }
}
