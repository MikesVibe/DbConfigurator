using DbConfigurator.Model.DTOs.Core;
using DbConfigurator.Model.DTOs.Wrapper;
using DbConfigurator.UI.Services.Interfaces;
using DbConfigurator.UI.Startup;
using DbConfigurator.UI.ViewModel.Base;
using Prism.Commands;
using Prism.Events;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;

namespace DbConfigurator.UI.Features.Regions
{
    public class RegionDetailViewModel : DetailViewModelBase<IRegionService, RegionDto>
    {
        private AreaDto? _selectedArea;
        private BuisnessUnitDto? _selectedBuisnessUnit;
        private CountryDto? _selectedCountry;

        public RegionDetailViewModel(
            IRegionService dataService,
            IEventAggregator eventAggregator
            ) : base(dataService, eventAggregator)
        {
            SelectedAreaChanged = new DelegateCommand(OnSelectedAreaChanged);
            SelectedBuisnessUnitChanged = new DelegateCommand(OnSelectedBuisnessUnitChanged);
            SelectedCountryChanged = new DelegateCommand(OnSelectedCountryChanged);

            Title = "Region";
            ViewWidth = 750;
            ViewHeight = 410;
        }

        public ICommand SelectedAreaChanged { get; set; }
        public ICommand SelectedBuisnessUnitChanged { get; set; }
        public ICommand SelectedCountryChanged { get; set; }

        public ObservableCollection<CountryDto> Countries_ObservableCollection { get; set; } = new ObservableCollection<CountryDto>();
        public ObservableCollection<BuisnessUnitDto> BuisnessUnits_ObservableCollection { get; set; } = new ObservableCollection<BuisnessUnitDto>();
        public ObservableCollection<AreaDto> Areas_ObservableCollection { get; set; } = new ObservableCollection<AreaDto>();

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

        public async Task LoadAsync()
        {
            var areas = await DataService.GetAllAreasAsync();
            foreach (var area in areas)
            {
                Areas_ObservableCollection.Add(area);
            }

            var buisnessUnits = await DataService.GetAllBuisnessUnitsAsync();
            foreach (var buisnessUnit in buisnessUnits)
            {
                BuisnessUnits_ObservableCollection.Add(buisnessUnit);
            }

            var countries = await DataService.GetAllCountriesAsync();
            foreach (var country in countries)
            {
                Countries_ObservableCollection.Add(country);
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
        protected override bool OnSaveCanExecute()
        {
            return
                Region is not null &&
                Region.Area is not null &&
                Region.BuisnessUnit is not null &&
                Region.Country is not null;
        }
        private void OnSelectedCountryChanged()
        {
            if (Region == null)
                return;

            ((DelegateCommand)SaveCommand).RaiseCanExecuteChanged();
        }
        private void OnSelectedBuisnessUnitChanged()
        {
            if (Region == null)
                return;

            ((DelegateCommand)SaveCommand).RaiseCanExecuteChanged();
        }
        private void OnSelectedAreaChanged()
        {
            if (Region == null)
                return;

            ((DelegateCommand)SaveCommand).RaiseCanExecuteChanged();
        }

        protected override void OnCreate()
        {
            throw new System.NotImplementedException();
        }

        protected override void OnUpdate()
        {
            throw new System.NotImplementedException();
        }
    }
}
