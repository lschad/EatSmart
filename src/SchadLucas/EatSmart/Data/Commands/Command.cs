using System;
using System.Windows.Input;

namespace SchadLucas.EatSmart.Data.Commands
{
    public abstract class Command : ICommand, IRaiseCanExecuteChanged
    {
        protected readonly Predicate<object> _canExecute;
        protected readonly Action<object> _execute;
        protected bool _isExecuting;

        public virtual event EventHandler CanExecuteChanged
        {
            add => CommandManager.RequerySuggested += value;
            remove => CommandManager.RequerySuggested -= value;
        }

        protected Command(Action<object> execute, Predicate<object> canExecute = null)
        {
            _execute = execute ?? throw new ArgumentNullException(nameof(execute));
            _canExecute = canExecute;
        }

        public abstract bool CanExecute(object parameter);

        public abstract void Execute(object parameter);

        /// <inheritdoc />
        public virtual void RaiseCanExecuteChanged()
        {
            CommandManager.InvalidateRequerySuggested();
        }
    }
}