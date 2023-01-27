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
        public MainViewModel(
            INavigationViewModel navigationViewModel,
            IRecipientDetailViewModel recipientDetailViewModel
            )
        {
            _navigationViewModel = navigationViewModel;
            _recipientDetailViewModel = recipientDetailViewModel;
            SelectedDetailViewModel = RecipientDetailViewModel;

        }

        public ObservableCollection<IRecipientDetailViewModel> DetailViewModels { get; }

        public INavigationViewModel NavigationViewModel
        {
            get { return _navigationViewModel; }
            set { _navigationViewModel = value; }
        }
        public IRecipientDetailViewModel RecipientDetailViewModel
        {
            get { return _recipientDetailViewModel; }
            set { _recipientDetailViewModel = value; }
        }
        public IRecipientDetailViewModel SelectedDetailViewModel
        {
            get { return _selectedDetailViewModel; }
            set { _selectedDetailViewModel = value; }
        }

        public async Task LoadAsync()
        {
            await NavigationViewModel.LoadAsync();
            await RecipientDetailViewModel.LoadAsync();
        }

        private IRecipientDetailViewModel _selectedDetailViewModel;
        private INavigationViewModel _navigationViewModel;
        private IRecipientDetailViewModel _recipientDetailViewModel;


    }
}
