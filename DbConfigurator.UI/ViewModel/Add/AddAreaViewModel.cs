using DbConfigurator.Model.DTOs;
using DbConfigurator.Model.Entities;
using DbConfigurator.Model.Wrapper.DTOs;
using DbConfigurator.UI.ViewModel.Base;
using DbConfigurator.UI.Windows;
using Prism.Commands;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace DbConfigurator.UI.ViewModel.Add
{
    public class AddAreaViewModel : EditingViewModelBase, IEditingViewModel, INotifyPropertyChanged
    {
        public AreaDtoWrapper Area { get; set; }

        public AddAreaViewModel()
        {
            AreaDto area = new();
            Area = new(area);
            Area.Name = "";

        }

    }
}
