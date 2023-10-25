using DbConfigurator.UI.ViewModel;
using System.Windows;

namespace DbConfigurator.UI.Windows
{
    public partial class MainWindow : Window
    {
        private MainWindowViewModel _viewModel;

        //private IDistributionInformationRepository? _repository;

        public MainWindow(MainWindowViewModel viewModel)
        {
            InitializeComponent();
            _viewModel = viewModel;
            DataContext = _viewModel;
            Loaded += MainWindow_Loaded;
        }

        private async void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            await _viewModel.LoadAsync();
        }
    }
}
