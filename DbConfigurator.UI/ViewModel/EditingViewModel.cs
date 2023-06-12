﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DbConfigurator.UI.ViewModel
{
    class EditingViewModel : ViewModelBase
    {
        public IEditingViewModel? SelectedEditingViewModel 
        { 
            get
            {
                return _selectedEditingViewModel;
            }
            set
            {
                _selectedEditingViewModel = value;
                OnPropertyChanged();
            }
        }

        private IEditingViewModel? _selectedEditingViewModel;
    }
}