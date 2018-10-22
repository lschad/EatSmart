using SchadLucas.EatSmart.Data;
using System.Data.SqlClient;

namespace SchadLucas.EatSmart.Services
{
    public class DbSqlFactoryService : Service, IDbSqlFactoryService
    {
        private readonly SqlConnectionStringBuilder _connectionBuilder;

        public DbSqlFactoryService(IDbConfigProviderService dbConfigProvider)
        {
            _connectionBuilder = new SqlConnectionStringBuilder
            {
                DataSource = dbConfigProvider.Host,
                UserID = dbConfigProvider.UserId,
                Password = dbConfigProvider.Password,
                InitialCatalog = dbConfigProvider.Database,
                IntegratedSecurity = dbConfigProvider.IntegratedSecurity,
                MultipleActiveResultSets = dbConfigProvider.MultipleActiveResultSets,
                Encrypt = dbConfigProvider.Encrypt
            };
        }

        public IApplicationDbContext Get() => new ApplicationSqlDbContext(_connectionBuilder.ConnectionString);
    }
}