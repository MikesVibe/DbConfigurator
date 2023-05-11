using DbConfigurator.Model;
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
        public CountryTableViewModel(IDataModel dataModel,
            IEventAggregator eventAggregator
            ) : base(eventAggregator)
        {
            _dataModel = dataModel;

            Countries_ObservableCollection = new ObservableCollection<CountryLookup>();
        }


        public override async Task LoadAsync()
        {
            var countries = _dataModel.Countries;

            foreach (var country in countries)
            {
                var wrapper = new CountryLookup(country);
                Countries_ObservableCollection.Add(wrapper);
            }

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
                //&& !SelectedCountry.HasErrors
                && HasChanges;
        }
        protected async override void OnSaveExecute()
        {
            await _dataModel.SaveChangesAsync();
            HasChanges = _dataModel.HasChanges();
            Id = SelectedCountry.Id;
        }

        protected override void OnAddExecute()
        {
            throw new NotImplementedException();
        }

        protected override void OnRemoveExecute()
        {
            throw new NotImplementedException();
        }

        protected override bool OnRemoveCanExecute()
        {
            throw new NotImplementedException();
        }

        public int DefaultRowIndex { get { return 0; } }
        public CountryLookup SelectedCountry
        {
            get { return _selectedCountry; }
            set
            {
                _selectedCountry = value;
            }
        }


        public ObservableCollection<CountryLookup> Countries_ObservableCollection { get; set; }


        private CountryLookup _selectedCountry;
        private IDataModel _dataModel;

    }
}
