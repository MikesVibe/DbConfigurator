using DbConfigurator.Model.DTOs.Core;
using DbConfigurator.Model.DTOs.Wrapper;
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
