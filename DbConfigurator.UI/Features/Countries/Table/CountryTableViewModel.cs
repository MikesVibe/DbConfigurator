using DbConfigurator.Model.DTOs.Core;
using DbConfigurator.Model.DTOs.Wrapper;
using DbConfigurator.UI.Event;
using DbConfigurator.UI.Features.Areas.Event;
using DbConfigurator.UI.Services.Interfaces;
using DbConfigurator.UI.Startup;
using DbConfigurator.UI.ViewModel.Base;
using DbConfigurator.UI.ViewModel.Interfaces;
using Prism.Events;
using System;

namespace DbConfigurator.UI.Features.Countries
{
    public class CountryTableViewModel : TableViewModelBase<CountryDtoWrapper, CountryDto, ICountryService>, ITableViewModel
    {

        public CountryTableViewModel(IEventAggregator eventAggregator,
            IEditingWindowService dialogService,
            ICountryService dataService,
            AutoMapperConfig autoMapper,
            Func<CountryDetailViewModel> countryDetailViewModelCreator)
            : base(eventAggregator, dialogService, dataService, countryDetailViewModelCreator, autoMapper)
        {
            SubscribeToCreateEvent<CreateCountryEvent, CreateCountryEventArgs>();
            SubscribeToEditEvent<EditCountryEvent, EditCountryEventArgs>();
        }
    }
}
