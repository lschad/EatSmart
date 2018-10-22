using Microsoft.EntityFrameworkCore;
using System;
using System.Diagnostics.CodeAnalysis;

namespace SchadLucas.EatSmart.Data
{
    [SuppressMessage("Microsoft.Design", "CA1063:ImplementIDisposableCorrectly",
        Justification = "False positive.  " +
                        "IDisposable is inherited via IFunctionality.  " +
                        "See http://stackoverflow.com/questions/8925925 for details.")]
    public class InMemoryApplicationDbContext : ApplicationDbContext, IInMemoryApplicationDbContext
    {
        private static readonly string Name = Guid.NewGuid().ToString();

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);

            optionsBuilder.UseInMemoryDatabase(Name);
        }
    }
}