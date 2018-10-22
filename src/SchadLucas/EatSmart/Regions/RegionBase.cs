using Autofac;
using JetBrains.Annotations;
using SchadLucas.EatSmart.Services;
using System;

namespace SchadLucas.EatSmart.Regions
{
    [UsedImplicitly]
    public abstract class RegionBase
    {
        private bool _registerd;

        public virtual void Register(IRegionManager regionManager, ILifetimeScope container)
        {
            if (_registerd)
            {
                throw new ArgumentException("Region is already registered.");
            }

            _registerd = true;
        }
    }
}