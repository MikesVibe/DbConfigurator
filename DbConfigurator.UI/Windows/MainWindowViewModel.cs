using Autofac.Features.Indexed;
using DbConfigurator.UI.Base.Contracts;
using DbConfigurator.UI.Event;
using DbConfigurator.UI.ViewModel.Base;
using DbConfigurator.UI.Windows;
using Prism.Events;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;

namespace DbConfigurator.UI.ViewModel
{
    public class MainWindowViewModel : WindowViewModelBase
    {
        private IIndex<string, IMainPanelViewModel> _mainViewModelCreator;
        private IEventAggregator _eventAggregator;
        private INavigationPanelViewModel _navigationViewModel;
        private IMainPanelViewModel? _selectedMainViewModel;
        private bool _openTableReady = true;

        public MainWindowViewModel(
            INavigationPanelViewModel navigationViewModel,
            IIndex<string, IMainPanelViewModel> tabelViewModelCreator,
            IEventAggregator eventAggregator,
            IStatusService statusService
            ) : base(statusService)
        {
            _mainViewModelCreator = tabelViewModelCreator;
            _eventAggregator = eventAggregator;
            _navigationViewModel = navigationViewModel;

            MainViewModels = new ObservableCollection<IMainPanelViewModel>();

            _eventAggregator.GetEvent<OpenPanelViewEvent>()
                .Subscribe(OnOpenPanelView);
        }

        public ObservableCollection<IMainPanelViewModel> MainViewModels { get; }
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
        private async void OnOpenPanelView(OpenPanelViewEventArgs args)
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

                tabelViewModel.SetId(args.Id);
                await tabelViewModel.LoadAsync();

                MainViewModels.Add(tabelViewModel);
            }
            else
            {
                await tabelViewModel.RefreshAsync();
            }

            SelectedMainPanelViewModel = tabelViewModel;
            _openTableReady = true;
        }
    }
}
