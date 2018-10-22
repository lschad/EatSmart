using NLog;
using JetBrains.Annotations;

namespace SchadLucas.EatSmart.Services
{
    [UsedImplicitly]
    public abstract class Service : IService
    {
        /// <summary>
        ///     Gets the <see cref="NLog.Logger" /> of the deriving Class
        /// </summary>
        protected Logger Logger => LogManager.GetLogger(GetType().Name);
    }
}