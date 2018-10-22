using SchadLucas.EatSmart.Data;
using JetBrains.Annotations;

namespace SchadLucas.EatSmart.Services
{
    [UsedImplicitly]
    public interface IDbFactoryService
    {
        IApplicationDbContext Get();
    }
}