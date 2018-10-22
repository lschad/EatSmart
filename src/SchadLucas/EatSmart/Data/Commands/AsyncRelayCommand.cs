using System;
using System.Threading.Tasks;
using System.Windows.Input;

namespace SchadLucas.EatSmart.Data.Commands
{
    public class AsyncRelayCommand<T> : IAsyncCommand<T>, ICommand
    {
        private readonly Func<T, Task> _executeMethod;
        private readonly RelayCommand _underlyingCommand;
        private bool _isExecuting;

        public event EventHandler CanExecuteChanged
        {
            add => _underlyingCommand.CanExecuteChanged += value;
            remove => _underlyingCommand.CanExecuteChanged -= value;
        }

        public ICommand Command => this;

        public AsyncRelayCommand(Func<T, Task> executeMethod, Predicate<object> canExecuteMethod)
        {
            _executeMethod = executeMethod;
            _underlyingCommand = new RelayCommand(_ => { }, canExecuteMethod);
        }

        public bool CanExecute(object parameter)
        {
            return !_isExecuting && _underlyingCommand.CanExecute((T)parameter);
        }

        public async void Execute(object parameter)
        {
            await ExecuteAsync((T)parameter).ConfigureAwait(false);
        }

        public async Task ExecuteAsync(T parameter)
        {
            try
            {
                _isExecuting = true;
                RaiseCanExecuteChanged();
                await _executeMethod(parameter).ConfigureAwait(false);
            }
            finally
            {
                _isExecuting = false;
                RaiseCanExecuteChanged();
            }
        }

        public void RaiseCanExecuteChanged()
        {
            _underlyingCommand.RaiseCanExecuteChanged();
        }
    }
}