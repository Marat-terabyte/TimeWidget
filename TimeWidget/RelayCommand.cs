using System;
using System.Windows.Input;

namespace TimeWidget
{
    class RelayCommand : ICommand
    {
        private readonly Action<object> _execute;
        public RelayCommand(Action<object> action)
        {
            _execute = action;
        }

        public event EventHandler? CanExecuteChanged;

        public bool CanExecute(object? parameter) => true;

        public void Execute(object? parameter)
        {
            _execute(parameter!);
        }
    }
}
