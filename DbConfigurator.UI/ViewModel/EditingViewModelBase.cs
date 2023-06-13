using DbConfigurator.UI.View.Add;
using Prism.Commands;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace DbConfigurator.UI.ViewModel
{
    public abstract class EditingViewModelBase : ViewModelBase, IEditingViewModel, INotifyPropertyChanged
    {
        public ICommand AddCommand { get; }
        public ICommand CancelCommand { get; }

        public Action<bool> CloseAction { get; set; }

        public EditingViewModelBase()
        { 
            AddCommand = new DelegateCommand(Add, CanAdd);
            CancelCommand = new DelegateCommand(Cancel);
        }

        public void Add()
        {
            CloseAction?.Invoke(true);
        }
        public bool CanAdd()
        {
            return true;
        }

        public void Cancel()
        {
            CloseAction?.Invoke(false);
        }

    }
}
