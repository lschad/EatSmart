using Microsoft.EntityFrameworkCore;
using NLog;
using SchadLucas.EatSmart.Data.Types.Entities;
using SchadLucas.EatSmart.Utilities;
using System;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Threading.Tasks;

namespace SchadLucas.EatSmart.Data
{
    [SuppressMessage("Microsoft.Design", "CA1063:ImplementIDisposableCorrectly",
        Justification = "False positive.  " +
                        "IDisposable is inherited via IFunctionality.  " +
                        "See http://stackoverflow.com/questions/8925925 for details.")]
    public abstract class ApplicationDbContext : DbContext, IApplicationDbContext
    {
        protected Logger Logger => LogManager.GetLogger(GetType().Name);

        public DbSet<Food> Foods { get; set; }
        public DbSet<Person> Persons { get; set; }

        #region SaveChanges

        private void OnEntityAdded(EntityBase entity)
        {
            Logger.Info($"The DbEntity <{entity} ({entity.Id})> was added.");
        }

        private void OnEntityDeleted(EntityBase entity)
        {
            Logger.Info($"The DbEntity <{entity} ({entity.Id})> has been deleted.");
        }

        private void OnEntityModified(EntityBase entity)
        {
            Logger.Info($"The DbEntity <{entity} ({entity.Id})> has been changed.");
            var entry = Entry(entity);
            var originalValues = entry.OriginalValues;
            var currentValues = entry.CurrentValues;

            foreach (var propertyName in originalValues.Properties)
            {
                var original = originalValues[propertyName];
                var current = currentValues[propertyName];

                if (!Equals(original, current))
                {
                    Logger.Debug($"Property <{propertyName}> changed from <{original}> to <{current}>");
                }
            }
        }

        public override int SaveChanges()
        {
            var entries = ChangeTracker.Entries();
            var changes = entries.Where(e => e.State != EntityState.Unchanged);

            foreach (var change in changes)
            {
                switch (change.State)
                {
                    case EntityState.Added:
                        OnEntityAdded((EntityBase)change.Entity);
                        break;

                    case EntityState.Modified:
                        OnEntityModified((EntityBase)change.Entity);

                        break;

                    case EntityState.Deleted:
                        OnEntityDeleted((EntityBase)change.Entity);
                        break;

                    case EntityState.Detached:
                    case EntityState.Unchanged:
                        break;

                    default:
                        throw new ArgumentOutOfRangeException();
                }
            }

            return base.SaveChanges();
        }

        public async Task<int> SaveChangesAsync()
        {
            return await AwaitableTask.RunAsync(SaveChanges);
        }

        #endregion SaveChanges
    }
}