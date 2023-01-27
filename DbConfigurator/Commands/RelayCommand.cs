using System;
using System.Windows.Input;

namespace DbConfigurator.UI.ViewModel
{
    public class RelayCommand : ICommand
    {
        public RelayCommand(Action<object> execute) : this(execute, null) { }
        public RelayCommand(Action<object> execute, Predicate<object> canExecute)
        {
            _execute = execute ?? throw new
            ArgumentNullException("execute"); _canExecute = canExecute;
        }

        public bool CanExecute(object parameter)
        {
            return _canExecute == null ? true : _canExecute(parameter);
        }

        public void Execute(object parameter) { _execute(parameter); }

        public event EventHandler CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }

        readonly Action<object> _execute;
        readonly Predicate<object> _canExecute;
    }
}
