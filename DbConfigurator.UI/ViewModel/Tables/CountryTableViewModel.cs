using DbConfigurator.Model.DTOs.Core;
using DbConfigurator.Model.DTOs.Wrapper;
using DbConfigurator.Model.Entities.Core;
using DbConfigurator.UI.Services.Interfaces;
using DbConfigurator.UI.Startup;
using DbConfigurator.UI.ViewModel.Detail;
using DbConfigurator.UI.ViewModel.Base;
using DbConfigurator.UI.ViewModel.Interfaces;
using Prism.Events;
using System.Threading.Tasks;

namespace DbConfigurator.UI.ViewModel.Tables
{
    public class CountryTableViewModel : TableViewModelBase<CountryDtoWrapper, CountryDto, ICountryService>, ITableViewModel
    {
        private readonly ICountryService _dataService;
        private readonly AutoMapperConfig _autoMapper;

        public CountryTableViewModel(IEventAggregator eventAggregator, IDialogService dialogService, ICountryService dataService, AutoMapperConfig autoMapper)
            : base(eventAggregator, dialogService, dataService)
        {
            _dataService = dataService;
            _autoMapper = autoMapper;
        }

        public override async Task LoadAsync()
        {
            var countries = await _dataService.GetAllAsync();
            foreach (var country in countries)
            {
                var mapped = _autoMapper.Mapper.Map<CountryDto>(country);
                var wrapped = new CountryDtoWrapper(mapped);
                Items.Add(wrapped);
            }
        }
        protected override void OnAddExecute()
        {
            //var addCountryViewModel = new CountryDetailViewModel();

            //bool? result = DialogService.ShowDialog(addCountryViewModel);

            //if (result == false)
            //    return;

            //var countryDtoWrapper = addCountryViewModel.Country;
            //var countryEntity = new Country
            //{
            //    CountryName = countryDtoWrapper.CountryName,
            //    CountryCode = countryDtoWrapper.CountryCode
            //};

            //_dataService.Add(countryEntity);
            //_dataService.SaveChanges();
            //var mapped = _autoMapper.Mapper.Map<CountryDto>(countryEntity);
            //var wrapped = new CountryDtoWrapper(mapped);
            //Items.Add(wrapped);
        }
        protected override void OnEditExecute()
        {
            //var addCountryViewModel = new CountryDetailViewModel();
            //addCountryViewModel.Country = SelectedItem!;

            //bool? result = DialogService.ShowDialog(addCountryViewModel);

            //if (result == false)
            //    return;

            //var countryDtoWrapper = addCountryViewModel.Country;
            //var countryEntity = _dataService.GetCountryById(countryDtoWrapper.Id);
            //if (countryEntity is null)
            //{
            //    //Log some error
            //    return;
            //}
            //countryEntity.CountryName = countryDtoWrapper.CountryName;
            //countryEntity.CountryCode = countryDtoWrapper.CountryCode;

            //_dataService.SaveChanges();


        }
        protected override void OnRemoveExecute()
        {
            //if (SelectedItem is null)
            //    return;

            //var country = _dataService.GetCountryById(SelectedItem.Id);
            //if (country is null)
            //{
            //    //Log some error mesage here
            //    return;
            //}

            //_dataService.Remove(country);
            //_dataService.SaveChanges();
            //base.OnRemoveExecute();
        }
    }
}
