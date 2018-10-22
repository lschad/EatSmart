using SchadLucas.EatSmart.UI;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SchadLucas.EatSmart.Services
{
    public class RegionManager : Service, IRegionManager
    {
        private IList<IRegion> Regions { get; } = new List<IRegion>();
        private Dictionary<string, List<object>> Views { get; } = new Dictionary<string, List<object>>();

        private IRegion GetRegionByName(string regionName)
        {
            return Regions.First(r => r.RegionName == regionName);
        }

        private void ThrowIfRegionDoesNotExist(string regionName)
        {
            if (!Regions.Any(r => r.RegionName == regionName))
            {
                throw new ArgumentException("regionName");
            }
        }

        public void Activate(string regionName, object view)
        {
            ThrowIfRegionDoesNotExist(regionName);

            if (!Views[regionName].Contains(view))
            {
                throw new ArgumentException(nameof(view));
            }

            Logger.Debug($"Activate view in Region '{regionName}'. (View: {view})");
            Regions.First(r => r.RegionName == regionName).RegionContent = view;
        }

        public void Attach(string regionName, object view)
        {
            ThrowIfRegionDoesNotExist(regionName);

            if (!Views.ContainsKey(regionName))
            {
                Views.Add(regionName, new List<object>());
            }

            Logger.Debug($"Attach view to Region '{regionName}'. (View: {view})");
            Views[regionName].Add(view);
        }

        public void Detach(string regionName, object view)
        {
            ThrowIfRegionDoesNotExist(regionName);

            Logger.Debug($"Detach view from Region '{regionName}'. (View: {view})");
            Views[regionName].Remove(view);
        }

        public void DetachAll(string regionName)
        {
            ThrowIfRegionDoesNotExist(regionName);

            Logger.Debug($"Detach all {Views[regionName].Count} views from Region '{regionName}'.");
            Views[regionName].Clear();
        }

        public bool HasView(string regionName, object view)
        {
            ThrowIfRegionDoesNotExist(regionName);

            return Views.ContainsKey(regionName) && Views[regionName].Contains(view);
        }

        public void Hide(string regionName)
        {
            ThrowIfRegionDoesNotExist(regionName);

            var region = GetRegionByName(regionName);
            region.Visible = false;
        }

        public void Register(IRegion region)
        {
            if (Regions.Any(r => r.RegionName == region.RegionName))
            {
                throw new ArgumentException($"A region with {region.RegionName} already exists.", nameof(region));
            }

            Logger.Debug($"Register new Region '{region.RegionName}'. (Region: {region})");
            Regions.Add(region);
        }

        public void SetDataContext(string regionName, object dataContext)
        {
            ThrowIfRegionDoesNotExist(regionName);

            Logger.Debug($"Set DataContext for Region '{regionName}'. (DataContext: {dataContext})");

            var region = Regions.First(r => r.RegionName == regionName);
            region.RegionContext = dataContext;
        }

        public void Show(string regionName)
        {
            ThrowIfRegionDoesNotExist(regionName);

            var region = GetRegionByName(regionName);
            region.Visible = true;
        }

        public void TryAttach(string regionName, object view)
        {
            if (!HasView(regionName, view))
            {
                Attach(regionName, view);
            }
        }
    }
}