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
        public CountryTableViewModel(IEventAggregator eventAggregator, IDialogService dialogService, ICountryService dataService, AutoMapperConfig autoMapper)
            : base(eventAggregator, dialogService, dataService)
        {
        }

        public override async Task LoadAsync()
        {
            var countries = await DataService.GetAllAsync();
            foreach (var country in countries)
            {
                var wrapped = new CountryDtoWrapper(country);
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
    }
}
