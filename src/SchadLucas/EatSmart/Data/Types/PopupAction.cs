using SchadLucas.EatSmart.Data.Commands;
using System;
using System.Windows.Input;

namespace SchadLucas.EatSmart.Data.Types
{
    public class PopupAction : IPopupAction
    {
        public event EventHandler Executed;
        public event EventHandler Executing;

        public ICommand Action { get; }
        public string Title { get; }

        public PopupAction(Action action, string title)
        {
            if (action == null)
            {
                throw new ArgumentNullException(nameof(action));
            }

            Action = new RelayCommand(p =>
            {
                OnExecuting();
                action();
                OnExecuted();
            });
            Title = title;
        }

        private void OnExecuted()
        {
            Executed?.Invoke(this, System.EventArgs.Empty);
        }

        private void OnExecuting()
        {
            Executing?.Invoke(this, System.EventArgs.Empty);
        }
    }
}