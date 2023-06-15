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
            INavigationPanelViewModel navigationViewModel,
            IIndex<string, IMainPanelViewModel> tabelViewModelCreator,
            IEventAggregator eventAggregator
            )
        {
            _mainViewModelCreator = tabelViewModelCreator;
            _eventAggregator = eventAggregator;
            _navigationViewModel = navigationViewModel;

            MainViewModels = new ObservableCollection<IMainPanelViewModel>();

            _eventAggregator.GetEvent<OpenTabelViewEvent>()
                .Subscribe(OnOpenTabelView);
        }

        private async void OnOpenTabelView(OpenTabelViewEventArgs args)
        {
            if (!_openTableReady)
                return;

            _openTableReady = false;

            var tabelViewModel = MainViewModels
             .SingleOrDefault(vm => vm.Id == args.Id &&
             vm.GetType().Name == args.ViewModelName);


            if (tabelViewModel == null)
            {
                tabelViewModel = _mainViewModelCreator[args.ViewModelName];
                await tabelViewModel.LoadAsync();

                MainViewModels.Add(tabelViewModel);
            }

            SelectedMainPanelViewModel = tabelViewModel;
            _openTableReady = true;
        }
        public INavigationPanelViewModel NavigationViewModel
        {
            get { return _navigationViewModel; }
            set { _navigationViewModel = value; }
        }
        public IMainPanelViewModel? SelectedMainPanelViewModel
        {
            get { return _selectedMainViewModel; }
            set
            {
                _selectedMainViewModel = value;
                OnPropertyChanged();
            }
        }

        public async Task LoadAsync()
        {
            await NavigationViewModel.LoadAsync();
        }

        public ObservableCollection<IMainPanelViewModel> MainViewModels { get; }

        private IIndex<string, IMainPanelViewModel> _mainViewModelCreator;
        private IEventAggregator _eventAggregator;
        private INavigationPanelViewModel _navigationViewModel;
        private IMainPanelViewModel? _selectedMainViewModel;
        private bool _openTableReady = true;
    }
}
