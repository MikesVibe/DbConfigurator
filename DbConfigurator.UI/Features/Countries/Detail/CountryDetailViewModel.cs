using DbConfigurator.Model.DTOs.Core;
using DbConfigurator.Model.DTOs.Wrapper;
using DbConfigurator.UI.Services.Interfaces;
using DbConfigurator.UI.ViewModel;
using DbConfigurator.UI.ViewModel.Base;
using Prism.Events;
using System.ComponentModel;

namespace DbConfigurator.UI.Features.Countries
{
    public class CountryDetailViewModel : DetailViewModelBase<ICountryService, CountryDto>, IDetailViewModel, INotifyPropertyChanged
    {
        public CountryDetailViewModel(ICountryService countryService, IEventAggregator eventAggregator) : base(countryService, eventAggregator)
        {
            Title = "Country";
            ViewWidth = 660;
            ViewHeight = 420;
        }

        protected override void OnCreate()
        {
            throw new System.NotImplementedException();
        }

        protected override void OnUpdate()
        {
            throw new System.NotImplementedException();
        }
    }
}
