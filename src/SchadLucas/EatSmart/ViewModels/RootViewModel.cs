using SchadLucas.EatSmart.Data.Messages;
using SchadLucas.EatSmart.Regions;
using SchadLucas.EatSmart.Services;

namespace SchadLucas.EatSmart.ViewModels
{
    public sealed class RootViewModel : ViewModel, IRootViewModel
    {
        private bool _isBlurred;

        private IRegionManager RegionManager { get; }

        public bool IsBlurred
        {
            get => _isBlurred;
            private set => SetField(ref _isBlurred, value);
        }

        public RootViewModel(IRegionManager regionManager, IMessageHubService messageHub)
        {
            RegionManager = regionManager;

            messageHub.Subscribe<ApplicationOverlayOpenedHubHubMessage>(OnOverlayOpened);
            messageHub.Subscribe<ApplicationOverlayClosedHubMessage>(OnOverlayClosed);
            messageHub.Subscribe<NavigatedHubMessage>(OnNavigatedHubMessage);
        }

        private void OnNavigatedHubMessage(NavigatedHubMessage msg)
        {
            RegionManager.TryAttach(RegionNames.Main, msg.View);
            // todo: when you set the new datacontext the old view will throw exceptions that
            // bindings are borked
            RegionManager.SetDataContext(RegionNames.Main, msg.ViewModel);
            RegionManager.Activate(RegionNames.Main, msg.View);
        }

        private void OnOverlayClosed(ApplicationOverlayClosedHubMessage obj)
        {
            IsBlurred = false;
            RegionManager.Hide(RegionNames.ApplicationOverlay);
        }
        private void OnOverlayOpened(ApplicationOverlayOpenedHubHubMessage msg)
        {
            IsBlurred = true;
            RegionManager.Show(RegionNames.ApplicationOverlay);
        }
    }
}