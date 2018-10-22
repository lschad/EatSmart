using NLog;
using System;

namespace SchadLucas.EatSmart.Data.Commands
{
    public class RelayCommand : Command
    {
        private static Logger Logger => LogManager.GetLogger(nameof(RelayCommand));

        public RelayCommand(Action<object> execute, Predicate<object> canExecute = null)
            : base(execute, canExecute)
        {
            Logger.Debug($"Created new Command. (execute: {execute.Method.Name}, canexecute: {canExecute?.Method.Name}");
        }

        public override bool CanExecute(object parameter)
        {
            var result = !_isExecuting && (_canExecute?.Invoke(parameter) ?? true);
            Logger.Debug($"({_execute.Method.Name}).CanExecute: {result}.");
            return result;
        }

        public override void Execute(object parameter)
        {
            Logger.Debug($"{_execute.Method.Name} gets executed.");

            _isExecuting = true;
            try
            {
                RaiseCanExecuteChanged();
                _execute(parameter);
            }
            catch (Exception e)
            {
                Logger.Debug($"{_execute.Method.Name} failed. \n{e} \n{e.InnerException}");
            }
            finally
            {
                _isExecuting = false;
                RaiseCanExecuteChanged();
            }
        }
    }
}