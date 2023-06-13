using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Autofac.Features.Indexed;
using DbConfigurator.Model;
using DbConfigurator.UI.Event;
using DbConfigurator.UI.Startup;
using DbConfigurator.UI.ViewModel.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Prism.Events;
using static DbConfigurator.DataAccess.DbConfiguratorDbContext;

namespace DbConfigurator.UI.ViewModel
{
    public class MainViewModel : ViewModelBase
    {
        public MainViewModel(
            INavigationViewModel navigationViewModel,
            IIndex<string, ITabelViewModel> tabelViewModelCreator,
            IEventAggregator eventAggregator
            )
        {
            _tableViewModelCreator = tabelViewModelCreator;
            _eventAggregator = eventAggregator;

            TableViewModels = new ObservableCollection<ITabelViewModel>();

            _eventAggregator.GetEvent<OpenTabelViewEvent>()
                .Subscribe(OnOpenTabelView);

            NavigationViewModel = navigationViewModel;
        }

        private async void OnOpenTabelView(OpenTabelViewEventArgs args)
        {
            if (!_openTableReady)
                return;

            _openTableReady = false;

            var tabelViewModel = TableViewModels
             .SingleOrDefault(vm => vm.Id == args.Id &&
             vm.GetType().Name == args.ViewModelName);


            if (tabelViewModel == null)
            {
                tabelViewModel = _tableViewModelCreator[args.ViewModelName];
                await tabelViewModel.LoadAsync();

                TableViewModels.Add(tabelViewModel);
            }

            SelectedTableViewModel = tabelViewModel;
            _openTableReady = true;
        }


        public INavigationViewModel NavigationViewModel
        {
            get { return _navigationViewModel; }
            set { _navigationViewModel = value; }
        }


        public ITabelViewModel SelectedTableViewModel
        {
            get { return _selectedTableViewModel; }
            set
            {
                _selectedTableViewModel = value;
                OnPropertyChanged();
            }
        }



        public async Task LoadAsync()
        {
            await NavigationViewModel.LoadAsync();
        }



        public ObservableCollection<ITabelViewModel> TableViewModels { get; }

        private IIndex<string, ITabelViewModel> _tableViewModelCreator;
        private IEventAggregator _eventAggregator;
        private INavigationViewModel _navigationViewModel;
        private ITabelViewModel _selectedTableViewModel;
        private bool _openTableReady = true;



    }
}
