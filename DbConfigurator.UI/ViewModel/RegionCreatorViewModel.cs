using DbConfigurator.Model;
using DbConfigurator.Model.DTOs;
using DbConfigurator.UI.Startup;
using DbConfigurator.UI.ViewModel.Interfaces;
using DbConfigurator.UI.Wrapper;
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
    public class RegionCreatorViewModel : TableViewModelBase, IRegionCreatorTableViewModel
    {
        private readonly AutoMapperConfig _autoMapper;
        private readonly IEventAggregator _eventAggregator;
        private readonly IDataModel _dataModel;

        public RegionCreatorViewModel(IDataModel dataModel,
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
            //SelectedBuisnessUnit = BuisnessUnits_ObservableCollection?.Where(c => c.Id == SelectedRegion.BuisnessUnitId).FirstOrDefault();
            //SelectedArea = Areas_ObservableCollection?.Where(c => c.Id == SelectedRegion.AreaId).FirstOrDefault();
        }

        public override async Task LoadAsync()
        {
            var countries = await _dataModel.GetCountriesWithoutDefaultAsync();
            foreach (var country in countries)
            {
                var wrapper = _autoMapper.Mapper.Map<RegionDto>(country);
                Regions_ObservableCollection.Add(wrapper);
            }

            var areas = EnumerableToObservableCollection(await _dataModel.GetAreasWithoutDefaultAsync());
            foreach (var area in areas)
            {
                var wrapper = _autoMapper.Mapper.Map<AreaDto>(area);
                Areas_ObservableCollection.Add(wrapper);
            }

            var buisnessUnits = EnumerableToObservableCollection(await _dataModel.GetBuisnessUnitsWithoutDefaultAsync());
            foreach (var buisnessUnit in buisnessUnits)
            {
                var wrapper = _autoMapper.Mapper.Map<BuisnessUnitDto>(buisnessUnit);
                BuisnessUnits_ObservableCollection.Add(wrapper);
            }

            //var countries = EnumerableToObservableCollection(_dataModel.CountriesDto);
            //Country_Collection = countries;

            //foreach (var wrapper in Countries_ObservableCollection)
            //{
            //    wrapper.PropertyChanged -= Country_ObservableCollection_PropertyChanged;
            //}
            //Countries_ObservableCollection.Clear();

            //foreach (var country in countries)
            //{
            //    var wrapper = new CountryWrapper(country);
            //    Countries_ObservableCollection.Add(wrapper);
            //    wrapper.PropertyChanged += Country_ObservableCollection_PropertyChanged;
            //}
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



        protected override void OnAddExecute()
        {
            //throw new NotImplementedException();
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
