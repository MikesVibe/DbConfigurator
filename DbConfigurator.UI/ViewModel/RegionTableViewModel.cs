using DbConfigurator.Model;
using DbConfigurator.Model.DTOs;
using DbConfigurator.Model.Entities;
using DbConfigurator.Model.Wrapper;
using DbConfigurator.UI.Startup;
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
            Regions_ObservableCollection = new ObservableCollection<RegionDto>();

            SelectionChangedCommand = new DelegateCommand(OnSelectionChanged);
            SaveCommand = new DelegateCommand(OnSaveExecute);
        }

        private void OnSaveExecute()
        {

        }

        private void OnSelectionChanged()
        {
            SelectedArea = Areas_ObservableCollection?.Where(c => c.Id == SelectedRegion.Area.Id).FirstOrDefault();
            SelectedBuisnessUnit = BuisnessUnits_ObservableCollection?.Where(c => c.Id == SelectedRegion.BuisnessUnit.Id).FirstOrDefault();
            SelectedCountry = Countries_ObservableCollection?.Where(c => c.Id == SelectedRegion.Country.Id).FirstOrDefault();
        }

        public override async Task LoadAsync()
        {
            var regions = await _dataModel.GetAllRegionsAsync();
            foreach (var region in regions)
            {
                var wrapper = _autoMapper.Mapper.Map<RegionDto>(region);
                Regions_ObservableCollection.Add(wrapper);
            }

            var areas = EnumerableToObservableCollection(await _dataModel.GetAllAreasAsync());
            foreach (var area in areas)
            {
                var mapped = _autoMapper.Mapper.Map<AreaDto>(area);
                Areas_ObservableCollection.Add(mapped);
            }

            var buisnessUnits = EnumerableToObservableCollection(await _dataModel.GetAllBuisnessUnitsAsync());
            foreach (var buisnessUnit in buisnessUnits)
            {
                var mapped = _autoMapper.Mapper.Map<BuisnessUnitDto>(buisnessUnit);
                BuisnessUnits_ObservableCollection.Add(mapped);
            }

            var countries = EnumerableToObservableCollection(await _dataModel.GetAllCountriesAsync());
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



        protected override async void OnAddExecute()
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
            //var wrappedRegion = new RegionWrapper(distributionInformationDto);

            Regions_ObservableCollection.Add(regionDto);
            SelectedRegion = regionDto;
        }

        protected override void OnRemoveExecute()
        {
            //throw new NotImplementedException();
        }

        protected override bool OnRemoveCanExecute()
        {
            return false;
            //throw new NotImplementedException();
        }

        public int DefaultRowIndex { get { return 0; } }
        public RegionDto SelectedRegion
        {
            get { return _selectedRegion; }
            set
            {
                _selectedRegion = value;
                OnPropertyChanged();
            }
        }
        public CountryDto SelectedCountry
        {
            get { return _selectedCountry; }
            set
            {
                _selectedCountry = value;
                OnPropertyChanged();
            }
        }
        public BuisnessUnitDto SelectedBuisnessUnit
        {
            get { return _selectedBuisnessUnit; }
            set
            {
                _selectedBuisnessUnit = value;
                OnPropertyChanged();
            }
        }
        public AreaDto SelectedArea
        {
            get { return _selectedArea; }
            set
            {
                _selectedArea = value;
                OnPropertyChanged();
            }
        }

        public ICommand SelectionChangedCommand { get; set; }
        public ICommand SaveCommand { get; set; }


        public ObservableCollection<RegionDto> Regions_ObservableCollection { get; set; } = new ObservableCollection<RegionDto>();

        public ObservableCollection<CountryDto> Countries_ObservableCollection { get; set; } = new ObservableCollection<CountryDto>();
        public ObservableCollection<BuisnessUnitDto> BuisnessUnits_ObservableCollection { get; set; } = new ObservableCollection<BuisnessUnitDto>();
        public ObservableCollection<AreaDto> Areas_ObservableCollection { get; set; } = new ObservableCollection<AreaDto>();


        private RegionDto _selectedRegion;
        private BuisnessUnitDto _selectedBuisnessUnit;
        private AreaDto _selectedArea;
        private CountryDto _selectedCountry;
    }
}
