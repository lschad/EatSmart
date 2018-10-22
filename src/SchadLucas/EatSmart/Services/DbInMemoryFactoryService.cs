using SchadLucas.EatSmart.Data;

namespace SchadLucas.EatSmart.Services
{
    public class DbInMemoryFactoryService : Service, IDbInMemoryFactoryService
    {
        public IApplicationDbContext Get() => new InMemoryApplicationDbContext();
    }
}