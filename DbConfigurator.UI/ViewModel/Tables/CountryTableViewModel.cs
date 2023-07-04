using DbConfigurator.Model;
using DbConfigurator.Model.DTOs.Core;
using DbConfigurator.Model.DTOs.Wrapper;
using DbConfigurator.Model.Entities.Core;
using DbConfigurator.UI.Services;
using DbConfigurator.UI.Startup;
using DbConfigurator.UI.ViewModel.Add;
using DbConfigurator.UI.ViewModel.Base;
using DbConfigurator.UI.ViewModel.Interfaces;
using Prism.Events;
using System.Threading.Tasks;

namespace DbConfigurator.UI.ViewModel.Tables
{
    public class CountryTableViewModel : TableViewModelBase<CountryDtoWrapper>, ITableViewModel
    {
        private readonly IDataService _dataModel;
        private readonly AutoMapperConfig _autoMapper;

        public CountryTableViewModel(IEventAggregator eventAggregator, IDialogService dialogService, IDataService dataModel, AutoMapperConfig autoMapper)
            : base(eventAggregator, dialogService)
        {
            _dataModel = dataModel;
            _autoMapper = autoMapper;
        }

        public override async Task LoadAsync()
        {
            var countries = await _dataModel.GetAllCountriesAsync();
            foreach (var country in countries)
            {
                var mapped = _autoMapper.Mapper.Map<CountryDto>(country);
                var wrapped = new CountryDtoWrapper(mapped);
                Items.Add(wrapped);
            }
        }
        protected override void OnAddExecute()
        {
            var addCountryViewModel = new AddCountryViewModel();

            bool? result = DialogService.ShowDialog(addCountryViewModel);

            if (result == false)
                return;

            var countryDtoWrapper = addCountryViewModel.Country;
            var countryEntity = new Country
            {
                CountryName = countryDtoWrapper.CountryName,
                CountryCode = countryDtoWrapper.CountryCode
            };

            _dataModel.Add(countryEntity);
            _dataModel.SaveChanges();
            var mapped = _autoMapper.Mapper.Map<CountryDto>(countryEntity);
            var wrapped = new CountryDtoWrapper(mapped);
            Items.Add(wrapped);
        }
        protected override void OnEditExecute()
        {
            var addCountryViewModel = new AddCountryViewModel();
            addCountryViewModel.Country = SelectedItem!;

            bool? result = DialogService.ShowDialog(addCountryViewModel);

            if (result == false)
                return;

            var countryDtoWrapper = addCountryViewModel.Country;
            var countryEntity = _dataModel.GetCountryById(countryDtoWrapper.Id);
            if (countryEntity is null)
            {
                //Log some error
                return;
            }
            countryEntity.CountryName = countryDtoWrapper.CountryName;
            countryEntity.CountryCode = countryDtoWrapper.CountryCode;

            _dataModel.SaveChanges();


        }
        protected override void OnRemoveExecute()
        {
            if (SelectedItem is null)
                return;

            var country = _dataModel.GetCountryById(SelectedItem.Id);
            if (country is null)
            {
                //Log some error mesage here
                return;
            }

            _dataModel.Remove(country);
            _dataModel.SaveChanges();
            base.OnRemoveExecute();
        }
    }
}
