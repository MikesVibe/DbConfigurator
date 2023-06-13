﻿using DbConfigurator.Model;
using DbConfigurator.Model.DTOs;
using DbConfigurator.Model.Entities;
using DbConfigurator.Model.Wrapper;
using DbConfigurator.Model.Wrapper.DTOs;
using DbConfigurator.UI.Startup;
using DbConfigurator.UI.ViewModel.Base;
using DbConfigurator.UI.ViewModel.Interfaces;
using Prism.Commands;
using Prism.Events;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace DbConfigurator.UI.ViewModel
{
    public class RegionTableViewModel : TableViewModelBase, IRegionCreatorTableViewModel
    {
        private readonly AutoMapperConfig _autoMapper;
        private readonly IEventAggregator _eventAggregator;
        private readonly IDataModel _dataModel;

        public RegionTableViewModel(IDataModel dataModel,
            IEventAggregator eventAggregator,
            AutoMapperConfig autoMapper
            ) : base(eventAggregator)
        {
            _dataModel = dataModel;
            _autoMapper = autoMapper;
            Regions_ObservableCollection = new ObservableCollection<RegionDtoWrapper>();

            SelectionChangedCommand = new DelegateCommand(OnSelectionChanged);
            SaveCommand = new DelegateCommand(OnSaveExecute);

            SelectedAreaChanged = new DelegateCommand(OnSelectedAreaChanged);
            SelectedBuisnessUnitChanged = new DelegateCommand(OnSelectedBuisnessUnitChanged);
            SelectedCountryChanged = new DelegateCommand(OnSelectedCountryChanged);
        }

        private void OnSelectedCountryChanged()
        {
            if(SelectedRegion == null || SelectedCountry == null)
                return;

            SelectedRegion.Country = SelectedCountry;
            var regionEntity = _dataModel.GetRegionById(SelectedRegion.Id);
            if(regionEntity == null)
                throw new ArgumentNullException(nameof(regionEntity));

            regionEntity.CountryId = SelectedCountry.Id;
            _dataModel.SaveChanges();
        }

        private void OnSelectedBuisnessUnitChanged()
        {
            if (SelectedRegion == null || SelectedBuisnessUnit == null)
                return;

            SelectedRegion.BuisnessUnit = SelectedBuisnessUnit;
            var regionEntity = _dataModel.GetRegionById(SelectedRegion.Id);
            if (regionEntity == null)
                throw new ArgumentNullException(nameof(regionEntity));

            regionEntity.BuisnessUnitId = SelectedBuisnessUnit.Id;
            _dataModel.SaveChanges();
        }

        private void OnSelectedAreaChanged()
        {
            if (SelectedRegion == null || SelectedArea == null)
                return;

            SelectedRegion.Area = SelectedArea;
            var regionEntity = _dataModel.GetRegionById(SelectedRegion.Id);
            if (regionEntity == null)
                throw new ArgumentNullException(nameof(regionEntity));

            regionEntity.AreaId = SelectedArea.Id;
            _dataModel.SaveChanges();
        }

        private void OnSaveExecute()
        {

        }

        private void OnSelectionChanged()
        {
            if (SelectedRegion != null)
            {
                SelectedArea = Areas_ObservableCollection.Where(c => c.Id == SelectedRegion.Area.Id).FirstOrDefault();
                SelectedBuisnessUnit = BuisnessUnits_ObservableCollection?.Where(c => c.Id == SelectedRegion.BuisnessUnit.Id).FirstOrDefault();
                SelectedCountry = Countries_ObservableCollection?.Where(c => c.Id == SelectedRegion.Country.Id).FirstOrDefault();
            }

            RemoveCommand.RaiseCanExecuteChanged();
        }

        public override async Task LoadAsync()
        {
            var regions = await _dataModel.GetAllRegionsAsync();
            foreach (var region in regions)
            {
                var mapped = _autoMapper.Mapper.Map<RegionDto>(region);
                var wrapped = new RegionDtoWrapper(mapped);
                Regions_ObservableCollection.Add(wrapped);
            }

            var areas = await _dataModel.GetAllAreasAsync();
            foreach (var area in areas)
            {
                var mapped = _autoMapper.Mapper.Map<AreaDto>(area);
                Areas_ObservableCollection.Add(mapped);
            }

            var buisnessUnits = await _dataModel.GetAllBuisnessUnitsAsync();
            foreach (var buisnessUnit in buisnessUnits)
            {
                var mapped = _autoMapper.Mapper.Map<BuisnessUnitDto>(buisnessUnit);
                BuisnessUnits_ObservableCollection.Add(mapped);
            }

            var countries = await _dataModel.GetAllCountriesAsync();
            foreach (var country in countries)
            {
                var mapped = _autoMapper.Mapper.Map<CountryDto>(country);
                Countries_ObservableCollection.Add(mapped);
            }
        }
        private void Country_ObservableCollection_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (!HasChanges)
            {
                HasChanges = _dataModel.HasChanges();
            }
            if (e.PropertyName == nameof(CountryWrapper.HasErrors))
            {
                //((DelegateCommand)SaveCommand).RaiseCanExecuteChanged();
            }
        }



        protected override async void OnAddAreaExecute()
        {
            //Create New Region
            var defaultArea = _dataModel.DefaultArea;
            var defaultBuisnessUnit = _dataModel.DefaultBuisnessUnit;
            var defaultCountry = _dataModel.DefaultCountry;
            var region = new Region
            {
                Area = defaultArea,
                BuisnessUnit = defaultBuisnessUnit,
                Country = defaultCountry
            };

            await _dataModel.AddAsync(region);
            await _dataModel.SaveChangesAsync();

            var regionDto = _autoMapper.Mapper.Map<RegionDto>(region);
            var wrappedRegion = new RegionDtoWrapper(regionDto);

            Regions_ObservableCollection.Add(wrappedRegion);
            SelectedRegion = wrappedRegion;
        }

        protected override void OnRemoveExecute()
        {
            var regionEntity = _dataModel.GetRegionById(SelectedRegion.Id);
            _dataModel.Remove(regionEntity!);
            _dataModel.SaveChanges();
            Regions_ObservableCollection.Remove(SelectedRegion);
            SelectedRegion = null;
        }

        protected override bool OnRemoveCanExecute()
        {
            return SelectedRegion != null;
        }

        public ICommand SelectionChangedCommand { get; set; }
        public ICommand SaveCommand { get; set; }

        public ICommand SelectedAreaChanged { get; set; }
        public ICommand SelectedBuisnessUnitChanged { get; set; }
        public ICommand SelectedCountryChanged { get; set; }

        public int DefaultRowIndex { get { return 0; } }

        public RegionDtoWrapper? SelectedRegion
        {
            get { return _selectedRegion; }
            set
            {
                _selectedRegion = value;
                OnPropertyChanged();
            }
        }
        public AreaDto? SelectedArea
        {
            get { return _selectedArea; }
            set
            {
                _selectedArea = value;
                OnPropertyChanged();
            }
        }
        public BuisnessUnitDto? SelectedBuisnessUnit
        {
            get { return _selectedBuisnessUnit; }
            set
            {
                _selectedBuisnessUnit = value;
                OnPropertyChanged();
            }
        }
        public CountryDto? SelectedCountry
        {
            get { return _selectedCountry; }
            set
            {
                _selectedCountry = value;
                OnPropertyChanged();
            }
        }

        public ObservableCollection<RegionDtoWrapper> Regions_ObservableCollection { get; set; } = new ObservableCollection<RegionDtoWrapper>();
        public ObservableCollection<CountryDto> Countries_ObservableCollection { get; set; } = new ObservableCollection<CountryDto>();
        public ObservableCollection<BuisnessUnitDto> BuisnessUnits_ObservableCollection { get; set; } = new ObservableCollection<BuisnessUnitDto>();
        public ObservableCollection<AreaDto> Areas_ObservableCollection { get; set; } = new ObservableCollection<AreaDto>();

        private RegionDtoWrapper? _selectedRegion;
        private BuisnessUnitDto? _selectedBuisnessUnit;
        private AreaDto? _selectedArea;
        private CountryDto? _selectedCountry;
    }
}
