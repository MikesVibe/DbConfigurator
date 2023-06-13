using DbConfigurator.Model.DTOs;
using DbConfigurator.Model.Wrapper.DTOs;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DbConfigurator.UI.ViewModel.Add
{
    public class AddCountryViewModel : EditingViewModelBase, INotifyPropertyChanged
    {
        public CountryDtoWrapper Country { get; set; }

        public AddCountryViewModel()
        {
            CountryDto country = new();
            Country = new(country);
            Country.CountryName = "";
        }

        private void CloseWindow(bool dialogResult)
        {

        }
    }
}
