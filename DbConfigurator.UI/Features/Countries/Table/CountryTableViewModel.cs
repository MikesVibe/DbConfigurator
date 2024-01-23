using DbConfigurator.Authentication;
using DbConfigurator.Model.DTOs.Wrapper;
using DbConfigurator.Core.Models;
using DbConfigurator.UI.Base.Contracts;
using DbConfigurator.UI.Event;
using DbConfigurator.UI.Features.Areas.Event;
using DbConfigurator.UI.Features.Countries.Services;
using DbConfigurator.UI.Startup;
using DbConfigurator.UI.ViewModel.Base;
using Prism.Events;
using System;

namespace DbConfigurator.UI.Features.Countries
{
    public class CountryTableViewModel : TableViewModelBase<CountryWrapper, Country, ICountryService,
        CreateCountryEvent, CreateCountryEventArgs,
        EditCountryEvent, EditCountryEventArgs>, ITableViewModel
    {

        public CountryTableViewModel(IEventAggregator eventAggregator,
            IEditingWindowService dialogService,
            ICountryService dataService,
            AutoMapperConfig autoMapper,
            Func<CountryDetailViewModel> countryDetailViewModelCreator,
            SecuritySettings securitySettings)
            : base(eventAggregator, dialogService, dataService, countryDetailViewModelCreator, autoMapper, securitySettings)
        {
        }
    }
}
