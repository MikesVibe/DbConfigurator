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
    public class CountryTableViewModel : TableViewModelBase, ICountryTableViewModel
    {
        public CountryTableViewModel(
            ICountryRepository countryRepository, 
            IEventAggregator eventAggregator
            ) : base(eventAggregator)
        {
            _countryRepository = countryRepository;

            Countries_ObservableCollection = new ObservableCollection<CountryWrapper>();
        }


        public async Task LoadAsync()
        {
            var buisnessUnits = await _countryRepository.GetAllAsync();

            foreach (var wrapper in Countries_ObservableCollection)
            {
                wrapper.PropertyChanged -= Country_ObservableCollection_PropertyChanged;
            }
            Countries_ObservableCollection.Clear();

            foreach (var buisnessUnit in buisnessUnits)
            {
                var wrapper = new CountryWrapper(buisnessUnit);
                Countries_ObservableCollection.Add(wrapper);
                wrapper.PropertyChanged += Country_ObservableCollection_PropertyChanged;
            }
        }
        private void Country_ObservableCollection_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (!HasChanges)
            {
                HasChanges = _countryRepository.HasChanges();
            }
            if (e.PropertyName == nameof(CountryWrapper.HasErrors))
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
            return SelectedCountry != null
                && !SelectedCountry.HasErrors
                && HasChanges;
        }
        protected override void OnSaveExecute()
        {
            _countryRepository.SaveAsync();
            HasChanges = _countryRepository.HasChanges();
            Id = SelectedCountry.Id;
        }


        public int DefaultRowIndex { get { return 0; } }
        public CountryWrapper SelectedCountry
        {
            get { return _selectedCountry; }
            set
            {
                _selectedCountry = value;
            }
        }
        public ObservableCollection<CountryWrapper> Countries_ObservableCollection { get; set; }


        private ICountryRepository _countryRepository;
        private CountryWrapper _selectedCountry;

    }
}
