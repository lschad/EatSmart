using Autofac;
using SchadLucas.EatSmart.Services;
using SchadLucas.EatSmart.UI.Views;
using SchadLucas.EatSmart.ViewModels;

namespace SchadLucas.EatSmart.Regions
{
    public sealed class ApplicationOverlayRegion : RegionBase
    {
        public override void Register(IRegionManager regionManager, ILifetimeScope container)
        {
            base.Register(regionManager, container);
            regionManager.Attach(RegionNames.ApplicationOverlay, container.Resolve<IApplicationOverlayView>());
            regionManager.Activate(RegionNames.ApplicationOverlay, container.Resolve<IApplicationOverlayView>());
            regionManager.SetDataContext(RegionNames.ApplicationOverlay, container.Resolve<IApplicationOverlayViewModel>());
            regionManager.Hide(RegionNames.ApplicationOverlay);
        }
    }
}