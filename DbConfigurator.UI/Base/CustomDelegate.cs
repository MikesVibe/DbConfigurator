using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace DbConfigurator.UI
{
    public class CustomDelegate : ICommand
    {

        private readonly Action<object> _executeAction;
        private readonly Func<bool> _canExecuteMethod;

        public CustomDelegate(Action<object> executeaction, Func<bool> canexecutemethod = null)
        {
            _executeAction = executeaction;
            _canExecuteMethod = canexecutemethod ?? (() => true);
        }


        public void Execute(object parameter = null) => _executeAction(parameter);

        public bool CanExecute(object parameter = null) => _canExecuteMethod?.Invoke() ?? true;

        public event EventHandler CanExecuteChanged;

        public void RaiseCanExecuteChanged()
        {
            CommandManager.InvalidateRequerySuggested();
            CanExecuteChanged?.Invoke(this, new EventArgs());
        }
    }
}
