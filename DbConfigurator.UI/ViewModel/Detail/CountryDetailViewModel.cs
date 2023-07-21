using DbConfigurator.Model.DTOs.Core;
using DbConfigurator.Model.DTOs.Wrapper;
using DbConfigurator.UI.Services.Interfaces;
using DbConfigurator.UI.ViewModel.Base;
using System.ComponentModel;

namespace DbConfigurator.UI.ViewModel.Detail
{
    public class CountryDetailViewModel : DetailViewModelBase<ICountryService, CountryDto>, IDetailViewModel, INotifyPropertyChanged
    {
        public CountryDetailViewModel(ICountryService countryService) : base(countryService)
        {
            CountryDto country = new();
            Country = new(country);
            Country.CountryName = "";
            Title = "Country";
            ViewWidth = 660;
            ViewHeight = 420;
        }

        public CountryDtoWrapper Country { get; set; }
    }
}
