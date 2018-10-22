using SchadLucas.EatSmart.Data.Messages;
using SchadLucas.EatSmart.Services;

namespace SchadLucas.EatSmart.ViewModels
{
    public sealed class ApplicationOverlayViewModel : ViewModel, IApplicationOverlayViewModel
    {
        private object _overlayContent;
        private object _overlayContext;

        public object OverlayContent
        {
            get => _overlayContent;
            private set => SetField(ref _overlayContent, value);
        }
        public object OverlayContext
        {
            get => _overlayContext;
            private set => SetField(ref _overlayContext, value);
        }

        public ApplicationOverlayViewModel(IMessageHubService messageHub)
        {
            messageHub.Subscribe<ApplicationOverlayRequestedHubMessage>(OnOverlayRequested);
            messageHub.Subscribe<ApplicationOverlayOpenedHubHubMessage>(OnOverlayOpened);
            messageHub.Subscribe<ApplicationOverlayClosedHubMessage>(OnOverlayClosed);
        }

        private void OnOverlayClosed(ApplicationOverlayClosedHubMessage obj)
        {
            OverlayContent = null;
        }

        private void OnOverlayOpened(ApplicationOverlayOpenedHubHubMessage obj)
        {
        }

        private void OnOverlayRequested(ApplicationOverlayRequestedHubMessage obj)
        {
            OverlayContent = obj.Content;
        }
    }
}