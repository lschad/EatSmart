using SchadLucas.EatSmart.UI.Views;
using SchadLucas.EatSmart.ViewModels.Dialogs;
using System;

namespace SchadLucas.EatSmart.Services
{
    public interface IApplicationOverlayService : IService
    {
        void CloseActiveOverlay();
        void OpenOverlay<TDialog, TContext>(Action<TDialog, TContext> closedCallback) where TDialog : IView where TContext : IApplicationOverlayDialog;
    }
}