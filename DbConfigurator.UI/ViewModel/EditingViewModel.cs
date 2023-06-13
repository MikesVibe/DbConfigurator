using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DbConfigurator.UI.ViewModel
{
    public class EditingViewModel : ViewModelBase
    {
        public EditingViewModel(IEditingViewModel editingViewModel) 
        {
            _selectedEditingViewModel = editingViewModel;
        }

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
