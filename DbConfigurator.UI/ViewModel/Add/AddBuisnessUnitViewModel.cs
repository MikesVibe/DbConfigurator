﻿using DbConfigurator.Model.DTOs.Core;
using DbConfigurator.Model.DTOs.Wrapper;
using DbConfigurator.UI.ViewModel.Base;
using System.ComponentModel;

namespace DbConfigurator.UI.ViewModel.Add
{
    public class AddBuisnessUnitViewModel : EditingViewModelBase, IEditingViewModel, INotifyPropertyChanged
    {
        public BuisnessUnitDtoWrapper BuisnessUnit { get; set; }

        public AddBuisnessUnitViewModel()
        {
            BuisnessUnitDto buisnessUnit = new();
            BuisnessUnit = new(buisnessUnit);
            BuisnessUnit.Name = "";
        }

        protected override bool CanAdd()
        {
            return true;
        }
    }
}
