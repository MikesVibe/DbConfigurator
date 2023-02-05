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
            _tabelViewModelCreator = tabelViewModelCreator;
            _eventAggregator = eventAggregator;

            TabelViewModels = new ObservableCollection<ITabelViewModel>();

            _eventAggregator.GetEvent<OpenTabelViewEvent>()
                .Subscribe(OnOpenTabelView);

            NavigationViewModel = navigationViewModel;
        }

        private async void OnOpenTabelView(OpenTabelViewEventArgs args)
        {
            if (!_openTableReady)
                return;

            _openTableReady = false;

            var tabelViewModel = TabelViewModels
             .SingleOrDefault(vm => vm.Id == args.Id &&
             vm.GetType().Name == args.ViewModelName);


            if (tabelViewModel == null)
            {
                tabelViewModel = _tabelViewModelCreator[args.ViewModelName];
                try
                {
                    await tabelViewModel.LoadAsync();
                }
                catch
                {
                    //await _messageDialogService.ShowInfoDialogAsync("Could not load the entity, " +
                    //    "maybe it was deleted in the meantime by another user. " +
                    //    "The navigation is refreshed for you.");
                    //await NavigationViewModel.LoadAsync();
                    return;
                }
                TabelViewModels.Add(tabelViewModel);
            }

            SelectedTableViewModel = tabelViewModel;
            _openTableReady = true;
        }

        public ObservableCollection<IRecipientTableViewModel> TableViewModels { get; }

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



        public ObservableCollection<ITabelViewModel> TabelViewModels { get; }

        private IIndex<string, ITabelViewModel> _tabelViewModelCreator;
        private IEventAggregator _eventAggregator;
        private INavigationViewModel _navigationViewModel;
        private ITabelViewModel _selectedTableViewModel;
        private bool _openTableReady = true;



    }
}
