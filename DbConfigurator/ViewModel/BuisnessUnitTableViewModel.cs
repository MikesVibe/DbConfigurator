using DbConfigurator.Model;
using DbConfigurator.UI.Data.Repositories;
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

namespace DbConfigurator.UI.ViewModel
{
    public class BuisnessUnitTableViewModel : TableViewModelBase, IBuisnessUnitTableViewModel
    {
        public BuisnessUnitTableViewModel(
            IBuisnessRepository buisnessUnitRepository,
            ICountryRepository countryRepository, 
            IEventAggregator eventAggregator
            ) : base(eventAggregator)
        {
            _buisnessUnitRepository = buisnessUnitRepository;
            _countryRepository = countryRepository;

            BuisnessUnit_ObservableCollection = new ObservableCollection<BuisnessUnitWrapper>();
            Countries_ObservableCollection = new ObservableCollection<Country>();
        }


        public async Task LoadAsync()
        {
            //var buisnessUnits = await _buisnessUnitRepository.GetAllAsync();



            //foreach (var wrapper in BuisnessUnit_ObservableCollection)
            //{
            //    wrapper.PropertyChanged -= BuisnessUnits_ObservableCollection_PropertyChanged; 

            //}
            //BuisnessUnit_ObservableCollection.Clear();

            //foreach (var buisnessUnit in buisnessUnits)
            //{
            //    var wrapper = new BuisnessUnitWrapper(buisnessUnit);
            //    BuisnessUnit_ObservableCollection.Add(wrapper);
            //    wrapper.PropertyChanged += BuisnessUnits_ObservableCollection_PropertyChanged;
            //}

            var countries = await _countryRepository.GetAllAsync();



            foreach (var country in countries)
            {
                Countries_ObservableCollection.Add(country);
            }
        }
        private void BuisnessUnits_ObservableCollection_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (!HasChanges)
            {
                HasChanges = _buisnessUnitRepository.HasChanges();
            }
            if (e.PropertyName == nameof(BuisnessUnitWrapper.HasErrors))
            {
                ((DelegateCommand)SaveCommand).RaiseCanExecuteChanged();
            }
        }

        protected override void OnDeleteExecute()
        {
            throw new NotImplementedException();
        }
        protected override bool OnSaveCanExecute()
        {
            return SelectedBuisnessUnit != null
                && !SelectedBuisnessUnit.HasErrors
                && HasChanges;
        }
        protected override void OnSaveExecute()
        {
            _buisnessUnitRepository.SaveAsync();
            HasChanges = _buisnessUnitRepository.HasChanges();
            Id = SelectedBuisnessUnit.Id;

        }


        public int DefaultRowIndex { get { return 0; } }
        public BuisnessUnitWrapper SelectedBuisnessUnit
        {
            get { return _selectedBuisnessUnit; }
            set
            {
                _selectedBuisnessUnit = value;
                OnPropertyChanged();
            }
        }
        public BuisnessUnitWrapper SelectedCountry
        {
            get { return _selectedBuisnessUnit; }
            set
            {
                _selectedBuisnessUnit = value;
            }
        }

        public ObservableCollection<BuisnessUnitWrapper> BuisnessUnit_ObservableCollection { get; set; }
        public ObservableCollection<Country> Countries_ObservableCollection { get; set; }


        private IBuisnessRepository _buisnessUnitRepository;
        private ICountryRepository _countryRepository;
        private BuisnessUnitWrapper _selectedBuisnessUnit;

    }
}
