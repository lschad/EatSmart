using JetBrains.Annotations;
using SchadLucas.EatSmart.UI;

namespace SchadLucas.EatSmart.Services
{
    [UsedImplicitly]
    public interface IRegionManager : IService
    {
        /// <summary>
        ///     Activates the passed <paramref name="view" />.
        /// </summary>
        /// <remarks>
        ///     The <paramref name="view" /> has to be <see cref="Attach">attached</see> to be <see cref="Activate">activated</see>.
        /// </remarks>
        /// <param name="regionName">The region which the view should be associated with.</param>
        /// <param name="view">The view which should be activated.</param>
        /// <exception cref="System.ArgumentException">
        ///     If the <paramref name="regionName" /> is not <see cref="Register">registered.</see>.
        /// </exception>
        /// <exception cref="System.ArgumentException">
        ///     If the <paramref name="view" /> is not <see cref="Attach">attached</see> to the region.
        /// </exception>
        void Activate(string regionName, object view);

        /// <summary>
        ///     Attaches a view to a region.
        /// </summary>
        /// <remarks>
        ///     A view can be associated with multiple regions. Although it's intended to use the
        ///     same instance with multiple regions.
        ///     <para />
        ///     A view does not automatically get <see cref="Activate">activated</see> if you attach
        ///     it to a region.
        /// </remarks>
        /// <param name="regionName">The region which the view should be associated with.</param>
        /// <param name="view">The view which should be attached.</param>
        /// <exception cref="System.ArgumentException">
        ///     If the <paramref name="regionName" /> is not <see cref="Register">registered.</see>.
        /// </exception>
        void Attach(string regionName, object view);

        /// <summary>
        ///     Detaches a view from a region.
        /// </summary>
        /// <param name="regionName">The region which the view is associated with.</param>
        /// <param name="view">The view which should be detached.</param>
        /// <exception cref="System.ArgumentException">
        ///     If the <paramref name="regionName" /> is not <see cref="Register">registered.</see>.
        /// </exception>
        void Detach(string regionName, object view);

        /// <summary>
        ///     Detaches all views from a region.
        /// </summary>
        /// <param name="regionName"></param>
        /// <exception cref="System.ArgumentException">
        ///     If the <paramref name="regionName" /> is not <see cref="Register">registered.</see>.
        /// </exception>
        void DetachAll(string regionName);

        /// <summary>
        ///     Checks if a region is already attached with a view.
        /// </summary>
        /// <param name="regionName">The region which is checked.</param>
        /// <param name="view">The view which is checked.</param>
        /// <returns><see langword="true" /> if the region has the passed view already attached.</returns>
        /// <exception cref="System.ArgumentException">
        ///     If the <paramref name="regionName" /> is not <see cref="Register">registered.</see>.
        /// </exception>
        bool HasView(string regionName, object view);

        /// <summary>
        ///     Hides the region.
        /// </summary>
        /// <remarks>
        ///     The visible space used by the region will be cleared. It won't just be <i>transparent</i>.
        /// </remarks>
        /// <param name="regionName">The name of the region which will be hidden.</param>
        /// <exception cref="System.ArgumentException">
        ///     If the <paramref name="regionName" /> is not <see cref="Register">registered.</see>.
        /// </exception>
        void Hide(string regionName);

        /// <summary>
        ///     Registers a new region.
        /// </summary>
        /// <exception cref="System.ArgumentException">
        ///     If a region with the same <see cref="IRegion.RegionName" /> is already registered.
        /// </exception>
        void Register(IRegion region);

        /// <summary>
        ///     Sets the DataContext of a region.
        /// </summary>
        /// <param name="regionName">The region whose context should be set.</param>
        /// <param name="dataContext">The context which should be set.</param>
        /// <exception cref="System.ArgumentException">
        ///     If the <paramref name="regionName" /> is not <see cref="Register">registered.</see>.
        /// </exception>
        void SetDataContext(string regionName, object dataContext);

        /// <summary>
        ///     Shows the region.
        /// </summary>
        /// <param name="regionName">The name of the region which will be shown.</param>
        /// <exception cref="System.ArgumentException">
        ///     If the <paramref name="regionName" /> is not <see cref="Register">registered.</see>.
        /// </exception>
        void Show(string regionName);

        /// <summary>
        ///     Attaches a view if it's not attached yet.
        /// </summary>
        /// <see cref="HasView" />
        /// <see cref="Attach" />
        /// <param name="regionName">The region which the view should be associated with.</param>
        /// <param name="view">The view which should be attached.</param>
        void TryAttach(string regionName, object view);
    }
}