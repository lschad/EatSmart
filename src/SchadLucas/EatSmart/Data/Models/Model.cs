using NLog;
using JetBrains.Annotations;
using SchadLucas.EatSmart.Services;
using System;
using System.Threading.Tasks;

namespace SchadLucas.EatSmart.Data.Models
{
    public abstract class Model : IModel
    {
        private readonly IDbFactoryService _contextFactory;

        [UsedImplicitly]
        protected Logger Logger => LogManager.GetLogger(GetType().Name);

        // ReSharper disable once SuggestBaseTypeForParameter
        protected Model(IDbFactoryService contextFactory)
        {
            _contextFactory = contextFactory;
        }

        protected void Query(Action<IApplicationDbContext> action)
        {
            using (var ctx = _contextFactory.Get())
            {
                action.Invoke(ctx);
            }
        }

        protected T Query<T>(Func<IApplicationDbContext, T> func)
        {
            using (var ctx = _contextFactory.Get())
            {
                return func.Invoke(ctx);
            }
        }

        protected async Task<T> QueryAsync<T>(Func<IApplicationDbContext, T> func)
        {
            return await Task.Run(() => Query(func)).ConfigureAwait(false);
        }

        protected async Task QueryAsync(Action<IApplicationDbContext> action)
        {
            await Task.Run(() => { Query(action); }).ConfigureAwait(false);
        }
    }
}