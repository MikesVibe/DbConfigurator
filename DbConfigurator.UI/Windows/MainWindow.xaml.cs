using DbConfigurator.DataAccess;
using DbConfigurator.Model;
using DbConfigurator.UI.ViewModel;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace DbConfigurator.UI.Windows
{
    public partial class MainWindow : Window
    {
        private MainViewModel _viewModel;
        private IDataModel _dataModel;
        private readonly ISeeder _seeder;

        //private IDistributionInformationRepository? _repository;

        public MainWindow(MainViewModel viewModel, IDataModel dataModel, ISeeder seeder)
        {
            InitializeComponent();
            _seeder = seeder;
            _viewModel = viewModel;
            _dataModel = dataModel;
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
