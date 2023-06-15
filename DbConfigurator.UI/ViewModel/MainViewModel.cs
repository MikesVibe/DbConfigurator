using Autofac.Features.Indexed;
using DbConfigurator.UI.Event;
using DbConfigurator.UI.ViewModel.Base;
using DbConfigurator.UI.ViewModel.Interfaces;
using Prism.Events;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;

namespace DbConfigurator.UI.ViewModel
{
    public class MainViewModel : ViewModelBase
    {
        public MainViewModel(
            INavigationViewModel navigationViewModel,
            IIndex<string, ITableViewModel> tabelViewModelCreator,
            IEventAggregator eventAggregator
            )
        {
            _tableViewModelCreator = tabelViewModelCreator;
            _eventAggregator = eventAggregator;

            TableViewModels = new ObservableCollection<ITableViewModel>();

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
        public ITableViewModel SelectedTableViewModel
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

        public ObservableCollection<ITableViewModel> TableViewModels { get; }

        private IIndex<string, ITableViewModel> _tableViewModelCreator;
        private IEventAggregator _eventAggregator;
        private INavigationViewModel _navigationViewModel;
        private ITableViewModel _selectedTableViewModel;
        private bool _openTableReady = true;
    }
}
