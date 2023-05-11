using DbConfigurator.DataAccess;
using DbConfigurator.Model;
using DbConfigurator.UI.ViewModel;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
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
        private IDataModel _dataModel;
        private readonly ISeeder _seeder;

        //private IDistributionInformationRepository? _repository;

        public MainWindow(MainViewModel viewModel, IDataModel dataModel, ISeeder seeder)
        {
            InitializeComponent();
            //_seeder = seeder;
            _viewModel = viewModel;
            _dataModel = dataModel;
            DataContext = _viewModel;
            Loaded += MainWindow_Loaded;
        }

        private async void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            //await _seeder.Seed();
            await _viewModel.LoadAsync();
        }

    }
}
