using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DbConfigurator.Model;
using DbConfigurator.UI.Startup;
using DbConfigurator.UI.ViewModel.Interfaces;
using Microsoft.EntityFrameworkCore;
using static DbConfigurator.DataAccess.DbConfiguratorDbContext;

namespace DbConfigurator.UI.ViewModel
{
    public class MainViewModel : ViewModelBase
    {
        public MainViewModel(
            INavigationViewModel navigationViewModel,
            IRecipientTableViewModel recipientDetailViewModel,
            IBuisnessUnitTableViewModel buisnessUnitTableViewModel
            )
        {
            _navigationViewModel = navigationViewModel;
            RecipientTableViewModel = recipientDetailViewModel;
            BuisnessUnitTableViewModel = buisnessUnitTableViewModel;


            SelectedTableViewModel = BuisnessUnitTableViewModel;
        }

        public ObservableCollection<IRecipientTableViewModel> TableViewModels { get; }

        public INavigationViewModel NavigationViewModel
        {
            get { return _navigationViewModel; }
            set { _navigationViewModel = value; }
        }
        public IRecipientTableViewModel RecipientTableViewModel
        {
            get { return _recipientDetailViewModel; }
            set { _recipientDetailViewModel = value; }
        }
        public IBuisnessUnitTableViewModel BuisnessUnitTableViewModel
        {
            get { return _buisnessUnitTableViewModel; }
            set { _buisnessUnitTableViewModel = value; }
        }
        public ITableViewModel SelectedTableViewModel
        {
            get { return _selectedTableViewModel; }
            set { _selectedTableViewModel = value; }
        }



        public async Task LoadAsync()
        {
            await NavigationViewModel.LoadAsync();
            await RecipientTableViewModel.LoadAsync();
            await BuisnessUnitTableViewModel.LoadAsync();
        }


        private ITableViewModel _selectedTableViewModel;

        private INavigationViewModel _navigationViewModel;
        private IRecipientTableViewModel _recipientDetailViewModel;
        private IBuisnessUnitTableViewModel _buisnessUnitTableViewModel;



    }
}
