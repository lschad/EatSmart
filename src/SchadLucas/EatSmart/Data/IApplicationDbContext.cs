using Microsoft.EntityFrameworkCore;
using SchadLucas.EatSmart.Data.Types.Entities;
using System;
using System.Threading.Tasks;

namespace SchadLucas.EatSmart.Data
{
    public interface IApplicationDbContext : IDisposable
    {
        DbSet<Food> Foods { get; set; }
        DbSet<Person> Persons { get; set; }

        int SaveChanges();

        Task<int> SaveChangesAsync();
    }
}