using DbConfigurator.UI.Windows;
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
            //ViewWidth = viewWidth;
            //ViewHeight = viewHeight;
            //Title = title;
            
            SaveCommand = new DelegateCommand(Add, CanAdd);
            CancelCommand = new DelegateCommand(Cancel);
        }
        public ICommand SaveCommand { get; }
        public ICommand CancelCommand { get; }
        public Action<bool>? CloseAction { get; set; }
        public int ViewWidth { get; private set; }
        public int ViewHeight { get; private set; }
        public string Title { get; private set; }

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
