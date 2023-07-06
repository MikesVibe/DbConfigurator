using DbConfigurator.DataAccess;
using DbConfigurator.UI.Services.Interfaces;
using DbConfigurator.UI.ViewModel;
using System.IO;
using System.Windows;

namespace DbConfigurator.UI.Windows
{
    public partial class MainWindow : Window
    {
        private MainViewModel _viewModel;
        private ICombinedDataService _dataService;
        private readonly ISeeder _seeder;

        //private IDistributionInformationRepository? _repository;

        public MainWindow(MainViewModel viewModel, ICombinedDataService dataModel, ISeeder seeder)
        {
            InitializeComponent();
            _seeder = seeder;
            _viewModel = viewModel;
            _dataService = dataModel;
            DataContext = _viewModel;
            Loaded += MainWindow_Loaded;
        }

        private async void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {

#if DEBUG
            if (!await _seeder.AnyRegionInDatabaseAsync())
            {
                var regionsAsJason = File.ReadAllText("Regions.json");

                await _seeder.SeedRegions(regionsAsJason);
            }
            if (!await _seeder.AnyRecipientInDatabaseAsync())
            {
                await _seeder.SeedRecipients();
            }
            if (!await _seeder.AnyDistributionInformationAsync())
            {
                await _seeder.SeedDistributionInformation();
            }

#endif

            await _viewModel.LoadAsync();
        }
    }
}
