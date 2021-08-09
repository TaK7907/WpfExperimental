using System;
using System.Windows.Input;

namespace WpfApp1
{
    internal class DelegateCommand : ICommand
    {
        private readonly Action _actionDelegate;
        private readonly Predicate<object> _canExecDelegate;

        public DelegateCommand(Action action, Predicate<object> canExec)
        {
            _actionDelegate = action;
            _canExecDelegate = canExec;
        }

        public bool CanExecute(object parameter) =>
            _canExecDelegate?.Invoke(parameter) == true;

        public void Execute(object parameter) =>
            _actionDelegate?.Invoke();

        public event EventHandler CanExecuteChanged;

        public void RaiseCanExecuteChanged() =>
            CanExecuteChanged?.Invoke(this, EventArgs.Empty);
    }
}
