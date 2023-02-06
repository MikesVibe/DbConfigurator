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


        }

        private void DistributionInformation_ObservableCollection_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (!HasChanges)
            {
                HasChanges = _distributionInformationRepository.HasChanges();
            }
            else
            {


            }
            if (e.PropertyName == nameof(DistributionInformationWrapper.HasErrors))
            {
                ((DelegateCommand)SaveCommand).RaiseCanExecuteChanged();
            }
        }

        protected override void OnDeleteExecute()
        {
            throw new NotImplementedException();
        }
        //protected override bool OnSaveCanExecute()
        //{
        //    return SelectedDisInfoLookup != null
        //        && !SelectedDisInfoLookup.HasErrors
        //        && HasChanges;
        //}
        //protected override void OnSaveExecute()
        //{
        //    _distributionInformationRepository.SaveAsync();
        //    HasChanges = _distributionInformationRepository.HasChanges();
        //    Id = SelectedDisInfoLookup.Id;

        //}
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


        public int DefaultRowIndex { get { return 0; } }
        public DistributionInformation SelectedDistributionInformation
        {
            get 
            {
                if (_selectedDistributionInformation != null)
                {
                    SelectedAreaIndex = Area_Collection.Where(a => a.Id == _selectedDistributionInformation.Country.BuisnessUnit.Area.Id).First().Id - 1;
                    SelectedBuisnessUnitIndex = BuisnessUnit_Collection.Where(b => b.Id == _selectedDistributionInformation.Country.BuisnessUnit.Id).First().Id - 1;
                    SelectedcCountryIndex = Country_Collection.Where(c => c.Id == _selectedDistributionInformation.Country.Id).First().Id - 1;
                    //SelectedcPriorityIndex = Priority_Collection.Where(p => p.Id == _selectedDistributionInformation.Priority.Id).First().Id - 1;

                }


                OnPropertyChanged(nameof(SelectedAreaIndex));
                OnPropertyChanged(nameof(SelectedBuisnessUnitIndex));
                OnPropertyChanged(nameof(SelectedcCountryIndex));
                //OnPropertyChanged(nameof(SelectedcPriorityIndex));

                return _selectedDistributionInformation; 
            }
            set
            {
                _selectedDistributionInformation = value;
                OnPropertyChanged();
            }
        }
        private int _selectedAreaIndex;
        public int SelectedAreaIndex
        {
            get { return _selectedAreaIndex; }
            set { _selectedAreaIndex = value; }
        }
        private int _selectedBuisnessUnitIndex;
        public int SelectedBuisnessUnitIndex
        {
            get { return _selectedBuisnessUnitIndex; }
            set { _selectedBuisnessUnitIndex = value; }
        }
        private int _selectedcCountryIndex;

        public int SelectedcCountryIndex
        {
            get { return _selectedcCountryIndex; }
            set { _selectedcCountryIndex = value; }
        }
        private int _selectedcPriorityIndex;

        public int SelectedcPriorityIndex
        {
            get { return _selectedcPriorityIndex; }
            set { _selectedcPriorityIndex = value; }
        }


        //private Area _selectedAreaItem;

        //public Area SelectedAreaItem
        //{
        //    get { return _selectedAreaItem; }
        //    set { _selectedAreaItem = value; }
        //}


        //public ObservableCollection<DistributionInformationWrapper> DistributionInformation_ObservableCollection { get; set; }

        //public DistributionInfoLookupWrapper SelectedDisInfoLookup
        //{
        //    get { return _selectedDisInfoLookup; }
        //    set
        //    {
        //        _selectedDisInfoLookup = value;
        //        OnPropertyChanged();
        //    }
        //}

        public ObservableCollection<DistributionInformation> DisInfoLookup_ObservableCollection { get; set; }
        public ObservableCollection<Area> Area_Collection { get; set; }
        public ObservableCollection<BuisnessUnit> BuisnessUnit_Collection { get; set; }
        public ObservableCollection<Country> Country_Collection { get; set; }
        public ObservableCollection<Priority> Priority_Collection { get; private set; }

        private IDistributionInformationRepository _distributionInformationRepository;
        private ICountryRepository _countryRepository;
        private IRecipientRepository _recipientRepository;
        private IEventAggregator _eventAggregator;
        private DistributionInformation _selectedDistributionInformation;
        //private DistributionInfoLookupWrapper _selectedDisInfoLookup;
    }
}
