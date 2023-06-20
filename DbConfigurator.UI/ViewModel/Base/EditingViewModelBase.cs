using Prism.Commands;
using System;
using System.ComponentModel;
using System.Windows.Input;

namespace DbConfigurator.UI.ViewModel.Base
{
    public abstract class EditingViewModelBase : ViewModelBase, IEditingViewModel, INotifyPropertyChanged
    {
        public EditingViewModelBase()
        {
            AddCommand = new DelegateCommand(Add, CanAdd);
            CancelCommand = new DelegateCommand(Cancel);
        }
        public ICommand AddCommand { get; }
        public ICommand CancelCommand { get; }
        public Action<bool>? CloseAction { get; set; }

        protected virtual void Add()
        {
            CloseAction?.Invoke(true);
        }
        protected virtual bool CanAdd()
        {
            return true;
        }
        protected virtual void Cancel()
        {
            CloseAction?.Invoke(false);
        }
    }
}
