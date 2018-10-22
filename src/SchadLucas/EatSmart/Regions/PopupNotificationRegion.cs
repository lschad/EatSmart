using Autofac;
using SchadLucas.EatSmart.Services;
using SchadLucas.EatSmart.UI.Views;
using SchadLucas.EatSmart.ViewModels;

namespace SchadLucas.EatSmart.Regions
{
    public sealed class PopupNotificationRegion : RegionBase
    {
        public override void Register(IRegionManager regionManager, ILifetimeScope container)
        {
            base.Register(regionManager, container);
            regionManager.Attach(RegionNames.PopupNotification, container.Resolve<IPopupNotificationView>());
            regionManager.Activate(RegionNames.PopupNotification, container.Resolve<IPopupNotificationView>());
            regionManager.SetDataContext(RegionNames.PopupNotification, container.Resolve<IPopupNotificationViewModel>());
        }
    }
}