using DbConfigurator.Model.DTOs.Core;
using DbConfigurator.Model.DTOs.Wrapper;
using DbConfigurator.UI.ViewModel.Base;
using System.ComponentModel;

namespace DbConfigurator.UI.ViewModel.Add
{
    public class AddCountryViewModel : EditingViewModelBase, IEditingViewModel, INotifyPropertyChanged
    {
        public AddCountryViewModel()
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
