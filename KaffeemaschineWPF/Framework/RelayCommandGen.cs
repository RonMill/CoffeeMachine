using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace KaffeemaschineWPF.Framework
{
    public class RelayCommandGen<T> : ICommand
    {
        private readonly Action<T> _execute;
        private readonly Func<bool> _canExecute;

        public RelayCommandGen(Action<T> execute , Func<bool> canexecute = null)
        {
            _execute = execute;
            _canExecute = canexecute;
        }

        public event EventHandler CanExecuteChanged
        {
            add => CommandManager.RequerySuggested += value;
            remove => CommandManager.RequerySuggested -= value;
        }

        public bool CanExecute(object parameter)
        {

            return _canExecute?.Invoke() ?? true;
        }

        public void Execute(object parameter)
        {
            _execute((T)parameter);
        }
    }
}
