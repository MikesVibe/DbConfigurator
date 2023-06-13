using DbConfigurator.Model.DTOs;
using DbConfigurator.Model.Wrapper;
using DbConfigurator.UI.Windows;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace DbConfigurator.UI.ViewModel
{
    class AddBuisnessUnitViewModel : EditingViewModelBase
    {
        public BuisnessUnitDto BuisnessUnit { get; set; }


        public ICommand AddCommand { get; }
        public ICommand CancelCommand { get; }

        public AddBuisnessUnitViewModel()
        {

        }

        private void Add(object parameter)
        {

            CloseWindow(true);
        }

        private bool CanAdd(object parameter)
        {
            return true;
        }

        private void Cancel(object parameter)
        {

        }

        private void CloseWindow(bool dialogResult)
        {

        }

    }
}
