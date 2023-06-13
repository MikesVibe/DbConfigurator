using DbConfigurator.Model.DTOs;
using DbConfigurator.Model.Wrapper.DTOs;
using DbConfigurator.UI.ViewModel.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
