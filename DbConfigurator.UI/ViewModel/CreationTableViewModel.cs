using DbConfigurator.Model;
using DbConfigurator.Model.DTOs;
using DbConfigurator.UI.Startup;
using DbConfigurator.UI.ViewModel.Interfaces;
using DbConfigurator.UI.Windows;
using Prism.Commands;
using Prism.Events;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace DbConfigurator.UI.ViewModel
{
    public class CreationTableViewModel : TableViewModelBase
    {
        private readonly IDataModel _dataModel;
        private readonly AutoMapperConfig _autoMapper;

        public ICommand AddCountryCommand { get; set; }
        public ICommand AddBuisnessUnitCommand { get; set; }
        public ICommand OpenRegionDetailsCommand { get; set; }

        public ObservableCollection<CountryDto> Countries { get; set; } = new();
        public ObservableCollection<BuisnessUnitDto> BuisnessUnits { get; set; } = new();
        public ObservableCollection<AreaDto> Areas { get; set; } = new();

        public CreationTableViewModel(IDataModel dataModel,
            IEventAggregator eventAggregator,
            AutoMapperConfig autoMapper
            ) : base(eventAggregator)
        {
            _dataModel = dataModel;
            _autoMapper = autoMapper;

            AddBuisnessUnitCommand = new DelegateCommand(OnAddBuisnessUnitExecute);
            AddCountryCommand = new DelegateCommand(OnAddCountryExecute);
            OpenRegionDetailsCommand = new DelegateCommand(OnOpenRegionDetailsCommand);
        }

        private void OnOpenRegionDetailsCommand()
        {
            AddAreaWindow areaWindow = new();
            AddAreaViewModel areaViewModel = new();

        }

        public override async Task LoadAsync()
        {
            var countries = await _dataModel.GetAllCountriesAsync(); 
            foreach(var country in countries)
            {
                var mapped = _autoMapper.Mapper.Map<CountryDto>(country);
                Countries.Add(mapped);
            }

            var buisnessUnits = await _dataModel.GetAllBuisnessUnitsAsync();
            foreach (var buisnessUnit in buisnessUnits)
            {
                var mapped = _autoMapper.Mapper.Map<BuisnessUnitDto>(buisnessUnit);
                BuisnessUnits.Add(mapped);
            }

            var areas = await _dataModel.GetAllAreasAsync();
            foreach (var area in areas)
            {
                var mapped = _autoMapper.Mapper.Map<AreaDto>(area);
                Areas.Add(mapped);
            }

        }

        protected override void OnAddAreaExecute()
        {
            AddAreaViewModel viewModel = new AddAreaViewModel();
            AddAreaWindow addAreaWindow = new AddAreaWindow();
            addAreaWindow.DataContext = viewModel;
            viewModel.Window = addAreaWindow;

            bool? result = addAreaWindow.ShowDialog();

            if (result == true)
            {
                // User clicked the Add button
                // Perform any actions with the entered area name here
                string areaName = viewModel.AreaName;
                // ...
            }
            else
            {
                // User clicked the Cancel button or closed the window
                // Handle cancellation logic here
            }
        }

        private void OnAddBuisnessUnitExecute()
        {
            AddBuisnessUnitViewModel viewModel = new AddBuisnessUnitViewModel();
            AddBuisnessUnitWindow addAreaWindow = new AddBuisnessUnitWindow();
            addAreaWindow.DataContext = viewModel;
            viewModel.Window = addAreaWindow;

            bool? result = addAreaWindow.ShowDialog();

            if (result == true)
            {
                // User clicked the Add button
                // Perform any actions with the entered area name here
                string areaName = viewModel.AreaName;
                // ...
            }
            else
            {
                // User clicked the Cancel button or closed the window
                // Handle cancellation logic here
            }
        }

        private void OnAddCountryExecute()
        {
            AddCountryViewModel viewModel = new AddCountryViewModel();
            AddCountryWindow addAreaWindow = new AddCountryWindow();
            addAreaWindow.DataContext = viewModel;
            viewModel.Window = addAreaWindow;

            bool? result = addAreaWindow.ShowDialog();

            if (result == true)
            {
                // User clicked the Add button
                // Perform any actions with the entered area name here
                string areaName = viewModel.AreaName;
                // ...
            }
            else
            {
                // User clicked the Cancel button or closed the window
                // Handle cancellation logic here
            }
        }

        protected override bool OnRemoveCanExecute()
        {
            return false;
        }

        protected override void OnRemoveExecute()
        {
            throw new NotImplementedException();
        }

    }
}
