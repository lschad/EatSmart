using Microsoft.EntityFrameworkCore;
using System.Diagnostics.CodeAnalysis;

namespace SchadLucas.EatSmart.Data
{
    [SuppressMessage("Microsoft.Design", "CA1063:ImplementIDisposableCorrectly",
        Justification = "False positive.  " +
                        "IDisposable is inherited via IFunctionality.  " +
                        "See http://stackoverflow.com/questions/8925925 for details.")]
    public sealed class ApplicationSqlDbContext : ApplicationDbContext, IApplicationSqlDbContext
    {
        private readonly string _connectionString;

        public ApplicationSqlDbContext(string connectionString)
        {
            _connectionString = connectionString;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
            optionsBuilder.UseSqlServer(_connectionString);
        }
    }
}