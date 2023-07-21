using DbConfigurator.Model.DTOs.Core;
using DbConfigurator.Model.DTOs.Wrapper;
using DbConfigurator.UI.Event;
using DbConfigurator.UI.Features.Areas.Event;
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

        public override async Task LoadAsync(int entityId)
        {
            await base.LoadAsync(entityId);

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

            if (EntityDto is not null)
            {
                if (EntityDto.Area is not null)
                    EntityDto.Area = Areas_ObservableCollection.Where(c => c.Id == EntityDto.Area.Id).FirstOrDefault() ?? EntityDto.Area;
                if (EntityDto.BuisnessUnit is not null)
                    EntityDto.BuisnessUnit = BuisnessUnits_ObservableCollection?.Where(c => c.Id == EntityDto.BuisnessUnit.Id).FirstOrDefault() ?? EntityDto.BuisnessUnit;
                if (EntityDto.Country is not null)
                    EntityDto.Country = Countries_ObservableCollection?.Where(c => c.Id == EntityDto.Country.Id).FirstOrDefault() ?? EntityDto.Country;
            }
        }
        protected override bool OnSaveCanExecute()
        {
            return
                EntityDto is not null &&
                EntityDto.Area is not null &&
                EntityDto.BuisnessUnit is not null &&
                EntityDto.Country is not null;
        }
        private void OnSelectedCountryChanged()
        {
            if (EntityDto == null)
                return;

            ((DelegateCommand)SaveCommand).RaiseCanExecuteChanged();
        }
        private void OnSelectedBuisnessUnitChanged()
        {
            if (EntityDto == null)
                return;

            ((DelegateCommand)SaveCommand).RaiseCanExecuteChanged();
        }
        private void OnSelectedAreaChanged()
        {
            if (EntityDto == null)
                return;

            ((DelegateCommand)SaveCommand).RaiseCanExecuteChanged();
        }

        protected override void OnCreate()
        {
            if (EntityDto is null)
                return;

            EventAggregator.GetEvent<CreateRegionEvent>()
                  .Publish(
                new CreateRegionEventArgs
                {
                    Region = new RegionDto
                    {
                        Id = EntityDto.Id,
                        Area = EntityDto.Area,
                        BuisnessUnit = EntityDto.BuisnessUnit,
                        Country = EntityDto.Country
                    }
                });
        }

        protected override void OnUpdate()
        {
            if (EntityDto is null)
                return;

            EventAggregator.GetEvent<EditRegionEvent>()
                  .Publish(
                new EditRegionEventArgs
                {
                    Region = new RegionDto
                    {
                        Id = EntityDto.Id,
                        Area = EntityDto.Area,
                        BuisnessUnit = EntityDto.BuisnessUnit,
                        Country = EntityDto.Country
                    }
                });
        }
    }
}
