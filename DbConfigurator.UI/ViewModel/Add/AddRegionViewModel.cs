using DbConfigurator.Model;
using DbConfigurator.Model.DTOs.Core;
using DbConfigurator.Model.DTOs.Wrapper;
using DbConfigurator.UI.Startup;
using DbConfigurator.UI.ViewModel.Base;
using Prism.Commands;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace DbConfigurator.UI.ViewModel.Add
{
    public class AddRegionViewModel : EditingViewModelBase
    {
        private readonly IDataModel _dataModel;
        private readonly AutoMapperConfig _autoMapper;

        public AddRegionViewModel(
            IDataModel dataModel,
            AutoMapperConfig autoMapper
            )
        {
            _dataModel = dataModel;
            _autoMapper = autoMapper;
            var region = new RegionDto();
            Region = new RegionDtoWrapper(region);

            SelectedAreaChanged = new DelegateCommand(OnSelectedAreaChanged);
            SelectedBuisnessUnitChanged = new DelegateCommand(OnSelectedBuisnessUnitChanged);
            SelectedCountryChanged = new DelegateCommand(OnSelectedCountryChanged);
        }

        public async Task LoadAsync()
        {
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

            if (Region is not null)
            {
                if (Region.Area is not null)
                    Region.Area = Areas_ObservableCollection.Where(c => c.Id == Region.Area.Id).FirstOrDefault() ?? Region.Area;
                if (Region.BuisnessUnit is not null)
                    Region.BuisnessUnit = BuisnessUnits_ObservableCollection?.Where(c => c.Id == Region.BuisnessUnit.Id).FirstOrDefault() ?? Region.BuisnessUnit;
                if (Region.Country is not null)
                    Region.Country = Countries_ObservableCollection?.Where(c => c.Id == Region.Country.Id).FirstOrDefault() ?? Region.Country;
            }

        }

        private void OnSelectedCountryChanged()
        {
            if (Region == null)
                return;

            ((DelegateCommand)AddCommand).RaiseCanExecuteChanged();
        }

        private void OnSelectedBuisnessUnitChanged()
        {
            if (Region == null)
                return;

            ((DelegateCommand)AddCommand).RaiseCanExecuteChanged();
        }

        private void OnSelectedAreaChanged()
        {
            if (Region == null)
                return;

            ((DelegateCommand)AddCommand).RaiseCanExecuteChanged();
        }

        protected override bool CanAdd()
        {
            return 
                Region is not null && 
                Region.Area is not null && 
                Region.BuisnessUnit is not null && 
                Region.Country is not null;
        }


        public ObservableCollection<CountryDto> Countries_ObservableCollection { get; set; } = new ObservableCollection<CountryDto>();
        public ObservableCollection<BuisnessUnitDto> BuisnessUnits_ObservableCollection { get; set; } = new ObservableCollection<BuisnessUnitDto>();
        public ObservableCollection<AreaDto> Areas_ObservableCollection { get; set; } = new ObservableCollection<AreaDto>();
        public ICommand SelectedAreaChanged { get; set; }
        public ICommand SelectedBuisnessUnitChanged { get; set; }
        public ICommand SelectedCountryChanged { get; set; }

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
        public RegionDtoWrapper? Region { get; set; }


        private AreaDto? _selectedArea;
        private BuisnessUnitDto? _selectedBuisnessUnit;
        private CountryDto? _selectedCountry;
    }
}
