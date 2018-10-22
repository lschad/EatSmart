using Autofac;
using SchadLucas.EatSmart.Services;
using SchadLucas.EatSmart.UI.Views;
using SchadLucas.EatSmart.ViewModels;

namespace SchadLucas.EatSmart.Regions
{
    public sealed class NavigationRegion : RegionBase
    {
        public override void Register(IRegionManager regionManager, ILifetimeScope container)
        {
            base.Register(regionManager, container);
            regionManager.Attach(RegionNames.Navigation, container.Resolve<INavigationView>());
            regionManager.Activate(RegionNames.Navigation, container.Resolve<INavigationView>());
            regionManager.SetDataContext(RegionNames.Navigation, container.Resolve<INavigationViewModel>());
        }
    }
}