using Autofac;
using JetBrains.Annotations;
using SchadLucas.EatSmart.Services;

namespace SchadLucas.EatSmart.Regions
{
    [UsedImplicitly]
    public interface IRegionBuilder : IService
    {
        void BuildRegion<T>() where T : RegionBase;
    }

    public class RegionBuilder : Service, IRegionBuilder
    {
        private readonly ILifetimeScope _container;
        private readonly IRegionManager _regionManager;

        public RegionBuilder(ILifetimeScope container, IRegionManager regionManager)
        {
            _container = container;
            _regionManager = regionManager;
        }

        public void BuildRegion<T>() where T : RegionBase
        {
            var region = _container.Resolve<T>();
            region.Register(_regionManager, _container);
        }
    }
}