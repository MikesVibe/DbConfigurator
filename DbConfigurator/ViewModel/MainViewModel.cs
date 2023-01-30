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
            IRecipientTableViewModel recipientDetailViewModel,
            IBuisnessUnitTableViewModel buisnessUnitTableDetailViewModel
            )
        {
            _navigationViewModel = navigationViewModel;
            _recipientDetailViewModel = recipientDetailViewModel;
            SelectedDetailViewModel = RecipientDetailViewModel;

        }

        public ObservableCollection<IRecipientTableViewModel> DetailViewModels { get; }

        public INavigationViewModel NavigationViewModel
        {
            get { return _navigationViewModel; }
            set { _navigationViewModel = value; }
        }
        public IRecipientTableViewModel RecipientDetailViewModel
        {
            get { return _recipientDetailViewModel; }
            set { _recipientDetailViewModel = value; }
        }
        public IRecipientTableViewModel SelectedDetailViewModel
        {
            get { return _selectedDetailViewModel; }
            set { _selectedDetailViewModel = value; }
        }

        public async Task LoadAsync()
        {
            await NavigationViewModel.LoadAsync();
            await RecipientDetailViewModel.LoadAsync();
        }

        private IRecipientTableViewModel _selectedDetailViewModel;
        private INavigationViewModel _navigationViewModel;
        private IRecipientTableViewModel _recipientDetailViewModel;


    }
}
