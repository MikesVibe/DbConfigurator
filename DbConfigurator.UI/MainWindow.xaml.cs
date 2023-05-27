﻿using DbConfigurator.DataAccess;
using DbConfigurator.Model;
using DbConfigurator.UI.ViewModel;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Diagnostics;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Reflection.Emit;
using System.Reflection.Metadata;
using System.Text.Json.Serialization;
using System.Windows;

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
                var parser = new CSVParser("Regions.csv");
                var regions = parser.Parse().ToList();


                var regionsAsJason = JsonConvert.SerializeObject(regions);

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
