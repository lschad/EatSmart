using System;

namespace SchadLucas.EatSmart.ViewModels.Dialogs
{
    public abstract class ApplicationOverlayDialog : ViewModel, IApplicationOverlayDialog
    {
        public event EventHandler Closed;

        protected virtual void OnClosed(object sender)
        {
            Closed?.Invoke(sender, EventArgs.Empty);
        }
        public virtual void Close()
        {
            OnClosed(this);
        }
    }
}