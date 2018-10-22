using System;

namespace SchadLucas.EatSmart.ViewModels.Dialogs
{
    public interface IApplicationOverlayDialog : IViewModel
    {
        event EventHandler Closed;
        void Close();
    }
}