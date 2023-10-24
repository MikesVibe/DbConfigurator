using DbConfigurator.Model.DTOs.Core;
using DbConfigurator.Model.Entities.Core;
using DbConfigurator.UI.Event;
using DbConfigurator.UI.Features.Areas.Event;
using DbConfigurator.UI.Services;
using DbConfigurator.UI.ViewModel.Base;
using Prism.Commands;
using Prism.Events;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;

namespace DbConfigurator.UI.Features.Regions
{
    public class RegionDetailViewModel : DetailViewModelBase<IRegionService, Region>
    {
        private Area? _selectedArea;
        private Country? _selectedBusinessUnit;
        private Country? _selectedCountry;

        public RegionDetailViewModel(
            IRegionService dataService,
            IEventAggregator eventAggregator
            ) : base(dataService, eventAggregator)
        {
            SelectedAreaChanged = new DelegateCommand(OnSelectedAreaChanged);
            SelectedBusinessUnitChanged = new DelegateCommand(OnSelectedBusinessUnitChanged);
            SelectedCountryChanged = new DelegateCommand(OnSelectedCountryChanged);

            Title = "Region";
            ViewWidth = 750;
            ViewHeight = 410;
        }

        public ICommand SelectedAreaChanged { get; set; }
        public ICommand SelectedBusinessUnitChanged { get; set; }
        public ICommand SelectedCountryChanged { get; set; }

        public ObservableCollection<Country> Countries_ObservableCollection { get; set; } = new ObservableCollection<Country>();
        public ObservableCollection<BusinessUnit> BusinessUnits_ObservableCollection { get; set; } = new ObservableCollection<BusinessUnit>();
        public ObservableCollection<Area> Areas_ObservableCollection { get; set; } = new ObservableCollection<Area>();

        public Area? SelectedArea
        {
            get { return _selectedArea; }
            set
            {
                _selectedArea = value;
                OnPropertyChanged();
            }
        }
        public Country? SelectedBusinessUnit
        {
            get { return _selectedBusinessUnit; }
            set
            {
                _selectedBusinessUnit = value;
                OnPropertyChanged();
            }
        }
        public Country? SelectedCountry
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

            var BusinessUnits = await DataService.GetAllBusinessUnitsAsync();
            foreach (var BusinessUnit in BusinessUnits)
            {
                BusinessUnits_ObservableCollection.Add(BusinessUnit);
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
                if (EntityDto.BusinessUnit is not null)
                    EntityDto.BusinessUnit = BusinessUnits_ObservableCollection?.Where(c => c.Id == EntityDto.BusinessUnit.Id).FirstOrDefault() ?? EntityDto.BusinessUnit;
                if (EntityDto.Country is not null)
                    EntityDto.Country = Countries_ObservableCollection?.Where(c => c.Id == EntityDto.Country.Id).FirstOrDefault() ?? EntityDto.Country;
            }
        }
        protected override bool OnSaveCanExecute()
        {
            return
                EntityDto is not null &&
                EntityDto.Area is not null &&
                EntityDto.BusinessUnit is not null &&
                EntityDto.Country is not null;
        }
        private void OnSelectedCountryChanged()
        {
            if (EntityDto == null)
                return;

            ((DelegateCommand)SaveCommand).RaiseCanExecuteChanged();
        }
        private void OnSelectedBusinessUnitChanged()
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
                    Entity = new Region
                    {
                        Id = EntityDto.Id,
                        Area = EntityDto.Area,
                        BusinessUnit = EntityDto.BusinessUnit,
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
                    Entity = new Region
                    {
                        Id = EntityDto.Id,
                        Area = EntityDto.Area,
                        BusinessUnit = EntityDto.BusinessUnit,
                        Country = EntityDto.Country
                    }
                });
        }
    }
}
