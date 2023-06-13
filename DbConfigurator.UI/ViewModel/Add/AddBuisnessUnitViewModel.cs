using DbConfigurator.Model;
using DbConfigurator.Model.DTOs;
using DbConfigurator.Model.Entities;
using DbConfigurator.Model.Wrapper;
using DbConfigurator.Model.Wrapper.DTOs;
using DbConfigurator.UI.Windows;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace DbConfigurator.UI.ViewModel.Add
{
    public class AddBuisnessUnitViewModel : EditingViewModelBase
    {
        public BuisnessUnitDtoWrapper BuisnessUnit { get; set; }

        public AddBuisnessUnitViewModel()
        {
            BuisnessUnitDto buisnessUnit = new();
            BuisnessUnit = new(buisnessUnit);
            BuisnessUnit.Name = "";
        }

        private void CloseWindow(bool dialogResult)
        {

        }

    }
}
