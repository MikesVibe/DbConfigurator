using DbConfigurator.Model.DTOs.Core;
using DbConfigurator.Model.DTOs.Wrapper;
using DbConfigurator.Model.Entities.Wrapper;
using DbConfigurator.UI.Event;
using DbConfigurator.UI.Features.Areas.Event;
using DbConfigurator.UI.Services.Interfaces;
using DbConfigurator.UI.Startup;
using DbConfigurator.UI.ViewModel.Base;
using DbConfigurator.UI.ViewModel.Interfaces;
using Prism.Events;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace DbConfigurator.UI.Features.Countries
{
    public class CountryTableViewModel : TableViewModelBase<CountryDtoWrapper, CountryDto, ICountryService>, ITableViewModel
    {

        public CountryTableViewModel(IEventAggregator eventAggregator,
            IWindowService dialogService,
            ICountryService dataService,
            AutoMapperConfig autoMapper,
            Func<CountryDetailViewModel> countryDetailViewModelCreator)
            : base(eventAggregator, dialogService, dataService, countryDetailViewModelCreator)
        {
            EventAggregator.GetEvent<CreateCountryEvent>()
                .Subscribe(OnCreateExecute);
            EventAggregator.GetEvent<EditCountryEvent>()
                .Subscribe(OnEditExecute);
        }


        private void OnCreateExecute(CreateCountryEventArgs obj)
        {
            var wrapped = new CountryDtoWrapper(obj.Country);
            Items.Add(wrapped);
        }
        private void OnEditExecute(EditCountryEventArgs obj)
        {
            var country = Items.Where(a => a.Id == obj.Country.Id).FirstOrDefault();
            if (country is null)
            {
                RefreshItemsList();
                return;
            }

            var countryDto = obj.Country;
            country.CountryName = countryDto.CountryName;
            country.CountryCode = countryDto.CountryCode;
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
    }
}
