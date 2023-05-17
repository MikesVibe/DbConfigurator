﻿using DbConfigurator.Model;
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

namespace DbConfigurator.UI.ViewModel
{
    public class CountryTableViewModel : TableViewModelBase, ICountryTableViewModel
    {
        public CountryTableViewModel(IDataModel dataModel,
            IEventAggregator eventAggregator,
            AutoMapperConfig autoMapper
            ) : base(eventAggregator)
        {
            _dataModel = dataModel;
            AutoMapper = autoMapper;
            Countries_ObservableCollection = new ObservableCollection<CountryDto>();
        }


        public override async Task LoadAsync()
        {
            var countries = await _dataModel.GetAllCountriesAsync();

            foreach (var country in countries)
            {
                if (country.Name == _dataModel.DefaultCountry.Name)
                    continue;

                var wrapper = AutoMapper.Mapper.Map<CountryDto>(country);
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
        public CountryDto SelectedCountry
        {
            get { return _selectedCountry; }
            set
            {
                _selectedCountry = value;
            }
        }


        public ObservableCollection<CountryDto> Countries_ObservableCollection { get; set; }
        private AutoMapperConfig AutoMapper { get; }

        private CountryDto _selectedCountry;
        private IDataModel _dataModel;

    }
}
