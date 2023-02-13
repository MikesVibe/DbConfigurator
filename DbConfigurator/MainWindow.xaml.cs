using DbConfigurator.DataAccess;
using DbConfigurator.Model;
using DbConfigurator.UI.Data.Repositories;
using DbConfigurator.UI.ViewModel;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Reflection.Emit;
using System.Reflection.Metadata;
using System.Windows;
using static DbConfigurator.DataAccess.DbConfiguratorDbContext;

namespace DbConfigurator.UI
{
    public partial class MainWindow : Window
    {
        private MainViewModel _viewModel;
        //private IDistributionInformationRepository? _repository;

        public MainWindow(MainViewModel viewModel)
        {
            InitializeComponent();
            _viewModel = viewModel;
            DataContext = _viewModel;
            Loaded += MainWindow_Loaded;

            //var services = new ServiceCollection();
            //services.AddDbContext<DbConfiguratorDbContext>(options =>
            //    options.UseSqlServer(ConfigurationManager.ConnectionStrings["DbConfiguration"].ConnectionString));
            //services.AddScoped<IDistributionInformationRepository, DistributionInformationRepository>();

            //var serviceProvider = services.BuildServiceProvider();
            //_repository = serviceProvider.GetService<IDistributionInformationRepository>();
        }

        private async void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            await _viewModel.LoadAsync();
        }

    }
}
