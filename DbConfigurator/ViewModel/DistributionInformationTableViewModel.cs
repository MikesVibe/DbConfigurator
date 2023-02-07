using DbConfigurator.DataAccess;
using DbConfigurator.Model;
using DbConfigurator.UI.Data.Repositories;
using DbConfigurator.UI.View;
using DbConfigurator.UI.Wrapper;
using Microsoft.VisualBasic;
using Prism.Commands;
using Prism.Events;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace DbConfigurator.UI.ViewModel
{
    public class DistributionInformationTableViewModel : TableViewModelBase, IDistributionInformationTableView
    {
        public DistributionInformationTableViewModel(
            IEventAggregator eventAggregator,
            IDistributionInformationRepository distributionInformationRepository,
            ICountryRepository countryRepository,
            IRecipientRepository recipientRepository
            ) : base(eventAggregator)
        {
            _distributionInformationRepository = distributionInformationRepository;
            _countryRepository = countryRepository;
            _recipientRepository = recipientRepository;

            DisInfoLookup_ObservableCollection = new ObservableCollection<DistributionInformation>();

            SelectionChangedCommand = new DelegateCommand(OnSelectionChanged);
        }

        public override async Task LoadAsync()
        {
            var distributionInformations = await _distributionInformationRepository.GetAllAsync();



            var distributionInformationsLookup = new ObservableCollection<DistributionInformation>();
            foreach (var dis in distributionInformations)
            {
                distributionInformationsLookup.Add(dis);
            }
            DisInfoLookup_ObservableCollection = distributionInformationsLookup;


            Area_Collection = new ObservableCollection<Area>();
            BuisnessUnit_Collection = new ObservableCollection<BuisnessUnit>();
            Country_Collection = new ObservableCollection<Country>();
            Priority_Collection = new ObservableCollection<Priority>();

            var areas = await _countryRepository.GetAllAreasAsync();
            foreach(var area in areas) 
            {
                Area_Collection.Add(area);
            }

            var buisnessUnits = await _countryRepository.GetAllBuisnessUnitsAsync();
            foreach (var bu in buisnessUnits)
            {
                BuisnessUnit_Collection.Add(bu);
            }
            var countries = await _countryRepository.GetAllCountriesAsync();
            foreach (var country in countries)
            {
                Country_Collection.Add(country);
            }
            var priorities = await _distributionInformationRepository.GetAllPrioritiesAsync();
            foreach (var priority in priorities)
            {
                Priority_Collection.Add(priority);
            }


        }



        protected override void OnDeleteExecute()
        {
            throw new NotImplementedException();
        }
        protected override bool OnSaveCanExecute()
        {
            return true;
        }
        protected override void OnSaveExecute()
        {
            _distributionInformationRepository.SaveAsync();
            HasChanges = _distributionInformationRepository.HasChanges();
            Id = SelectedDistributionInformation.Id;

        }
        private void DistributionInformation_ObservableCollection_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (!HasChanges)
            {
                HasChanges = _distributionInformationRepository.HasChanges();
            }
            if (e.PropertyName == nameof(DistributionInformationWrapper.HasErrors))
            {
                ((DelegateCommand)SaveCommand).RaiseCanExecuteChanged();
            }
        }
        private void OnSelectionChanged()
        {
            if (SelectedDistributionInformation != null)
            {
                var area = Area_Collection.Where(a => a.Id == SelectedDistributionInformation.Country.BuisnessUnit.Area.Id).FirstOrDefault();
                if (area != null)
                    SelectedAreaIndex = area.Id - 1;

                var buisnessUnit = BuisnessUnit_Collection.Where(b => b.Id == SelectedDistributionInformation.Country.BuisnessUnit.Id).FirstOrDefault();
                if (buisnessUnit != null)
                    SelectedBuisnessUnitIndex = buisnessUnit.Id - 1;

                var country = Country_Collection.Where(c => c.Id == SelectedDistributionInformation.Country.Id).FirstOrDefault();
                if (country != null)
                    SelectedcCountryIndex = country.Id - 1;

                var priority = Priority_Collection.Where(p => p.Id == SelectedDistributionInformation.Priority.Id).FirstOrDefault();
                if (priority != null)
                    SelectedcPriorityIndex = priority.Id - 1;

            }
        }


        public int DefaultRowIndex { get { return 0; } }
        public int SelectedAreaIndex
        {
            get { return _selectedAreaIndex; }
            set 
            {
                _selectedAreaIndex = value;
                OnPropertyChanged();
            }
        }
        public int SelectedBuisnessUnitIndex
        {
            get { return _selectedBuisnessUnitIndex; }
            set
            {
                _selectedBuisnessUnitIndex = value;
                OnPropertyChanged();
            }
        }
        public int SelectedcCountryIndex
        {
            get { return _selectedcCountryIndex; }
            set
            {
                _selectedcCountryIndex = value;
                OnPropertyChanged();
            }
        }
        public int SelectedcPriorityIndex
        {
            get { return _selectedcPriorityIndex; }
            set
            {
                _selectedcPriorityIndex = value;
                OnPropertyChanged();
            }
        }
        public DistributionInformation SelectedDistributionInformation
        {
            get { return _selectedDistributionInformation; }
            set
            {
                _selectedDistributionInformation = value;
                OnPropertyChanged();
            }
        }
        public ObservableCollection<DistributionInformation> DisInfoLookup_ObservableCollection { get; set; }
        public ObservableCollection<Area> Area_Collection { get; set; }
        public ObservableCollection<BuisnessUnit> BuisnessUnit_Collection { get; set; }
        public ObservableCollection<Country> Country_Collection { get; set; }
        public ObservableCollection<Priority> Priority_Collection { get; private set; }
        public ICommand SelectionChangedCommand { get; set; }



        private IDistributionInformationRepository _distributionInformationRepository;
        private ICountryRepository _countryRepository;
        private IRecipientRepository _recipientRepository;
        private IEventAggregator _eventAggregator;
        private DistributionInformation _selectedDistributionInformation;
        private int _selectedAreaIndex = -1;
        private int _selectedBuisnessUnitIndex = -1;
        private int _selectedcCountryIndex = -1;
        private int _selectedcPriorityIndex = -1;
    }
}
