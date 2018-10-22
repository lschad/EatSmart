using Autofac;
using SchadLucas.EatSmart.Data.Messages;
using SchadLucas.EatSmart.UI.Dialogs;
using SchadLucas.EatSmart.UI.Views;
using SchadLucas.EatSmart.Utilities;
using SchadLucas.EatSmart.ViewModels.Dialogs;
using System;

namespace SchadLucas.EatSmart.Services
{
    public class ApplicationOverlayService : Service, IApplicationOverlayService
    {
        private ILifetimeScope Container { get; }
        private IMessageHubService MessageHub { get; }

        // todo: replace IContainer with custom wrapper to avoid dependency? :thinkong:
        public ApplicationOverlayService(IMessageHubService messageHub, ILifetimeScope container)
        {
            MessageHub = messageHub;
            Container = container;
        }

        public void CloseActiveOverlay()
        {
            MessageHub.Publish<ApplicationOverlayClosedHubMessage>();
        }

        public void OpenOverlay<TDialog, TContext>(Action<TDialog, TContext> closedCallback) where TDialog : IView where TContext : IApplicationOverlayDialog
        {
            // Setup Views
            var wrapper = Container.Resolve<IDialogBase>();
            var wrapperContext = Container.Resolve<IDialogBaseViewModel>();
            ViewModelBinder.BindView(wrapperContext, wrapper);

            var dialog = Container.Resolve<TDialog>();
            var dialogContext = Container.Resolve<TContext>();
            ViewModelBinder.BindView(dialogContext, dialog);

            wrapper.InnerContent = dialog;

            void OnClosed(object sender, EventArgs e)
            {
                wrapperContext.Closed -= OnClosed;
                dialogContext.Closed -= OnClosed;

                CloseActiveOverlay();
                closedCallback(dialog, dialogContext);
            }

            wrapperContext.Closed += OnClosed;
            dialogContext.Closed += OnClosed;

            // Send Opened-HubMessage
            var msg = new ApplicationOverlayOpenedHubHubMessage(this, wrapper);
            MessageHub.Publish(msg);
        }
    }
}