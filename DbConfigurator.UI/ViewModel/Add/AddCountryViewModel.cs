using DbConfigurator.Model.DTOs;
using DbConfigurator.Model.Wrapper.DTOs;
using DbConfigurator.UI.ViewModel.Base;
using System.ComponentModel;

namespace DbConfigurator.UI.ViewModel.Add
{
    public class AddCountryViewModel : EditingViewModelBase, IEditingViewModel, INotifyPropertyChanged
    {
        public CountryDtoWrapper Country { get; set; }

        public AddCountryViewModel()
        {
            CountryDto country = new();
            Country = new(country);
            Country.CountryName = "";
        }

    }
}
