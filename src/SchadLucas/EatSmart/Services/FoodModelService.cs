using SchadLucas.EatSmart.Data.Models;
using SchadLucas.EatSmart.Data.Types.Entities;
using SchadLucas.EatSmart.Utilities;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Threading.Tasks;

namespace SchadLucas.EatSmart.Services
{
    [SuppressMessage("ReSharper", "ConsiderUsingConfigureAwait")]
    [SuppressMessage("ReSharper", "ClassTooBig")]
    [SuppressMessage("ReSharper", "UnusedMember.Global")]
    public class FoodModelService : Model, IFoodModelService
    {
        public FoodModelService(IDbInMemoryFactoryService contextFactory) : base(contextFactory)
        {
        }

        public void Add(Food food)
        {
            Logger.Debug($"Add food-entity '{food}'.");

            Query(ctx =>
            {
                ctx.Foods.Add(food);
                ctx.SaveChanges();
            });
        }

        public void AddRange(IEnumerable<Food> foods)
        {
            var list = foods.ToArray();
            Logger.Debug($"Add food-entities '{string.Join(", ", list.Select(f => f.Name))}'");

            Query(ctx =>
            {
                ctx.Foods.AddRange(list);
                ctx.SaveChanges();
            });
        }

        public void AddRange(params Food[] foods)
        {
            AddRange(foods.AsEnumerable());
        }

        public bool Delete(Guid id)
        {
            Logger.Debug($"Try do delete food-entity by Id: {id}.");

            var deleted = false;
            Query(ctx =>
            {
                var food = Find(id);
                if (food != null)
                {
                    ctx.Foods.Remove(food);
                    ctx.SaveChanges();

                    Logger.Debug($"Deleted food-entity ({id}) succesfully.");
                    deleted = true;
                }
            });

            return deleted;
        }

        public bool Delete(Food food) => Delete(food.Id);

        public bool Exists(Guid id)
        {
            Logger.Debug($"Check if food-entity {id} exists.");

            return Query(ctx => ctx.Foods.Any(f => f.Id == id));
        }

        public bool Exists(string name)
        {
            Logger.Debug($"Check if food-entity '{name}' exists.");

            return Query(ctx => ctx.Foods.Any(f => f.Name == name));
        }

        public bool Exists(Food food) => Exists(food.Id);

        public Food Find(Guid id)
        {
            Logger.Debug($"Try to find a food-entity by Id: {id}");

            return Query(ctx => ctx.Foods.FirstOrDefault(f => f.Id == id));
        }

        public Food Find(string name)
        {
            Logger.Debug($"Try to find a food-entity by name: {name}");

            return Query(ctx => ctx.Foods.FirstOrDefault(f => f.Name == name));
        }

        public IEnumerable<Food> GetAll()
        {
            Logger.Debug("Get all food-entities.");

            return Query(ctx => ctx.Foods.ToList());
        }

        public IEnumerable<Food> GetWhere(Func<Food, bool> condition)
        {
            Logger.Debug($"Get food entities where the following condition applies: {condition}");

            return Query(ctx => ctx.Foods.Where(condition).ToList());
        }

        #region Async Shortcuts

        public async Task AddAsync(Food food) => await AwaitableTask.RunAsync(() => Add(food));

        public async Task AddRangeAsync(IEnumerable<Food> foods) => await AwaitableTask.RunAsync(() => AddRange(foods));

        public async Task AddRangeAsync(Food[] foods) => await AddRangeAsync(foods.AsEnumerable());

        public async Task<bool> DeleteAsync(Guid id) => await AwaitableTask.RunAsync(() => Delete(id));

        public Task<bool> DeleteAsync(Food food) => DeleteAsync(food.Id);

        public async Task<bool> ExistsAsync(Guid id) => await AwaitableTask.RunAsync(() => Exists(id));

        public async Task<bool> ExistsAsync(string name) => await AwaitableTask.RunAsync(() => Exists(name));

        public async Task<bool> ExistsAsync(Food food) => await ExistsAsync(food.Id);

        public async Task<Food> FindAsync(Guid id) => await AwaitableTask.RunAsync(() => Find(id));

        public async Task<Food> FindAsync(string name) => await AwaitableTask.RunAsync(() => Find(name));

        public async Task<IEnumerable<Food>> GetAllAsync() => await AwaitableTask.RunAsync(GetAll);

        public async Task<IEnumerable<Food>> GetWhereAsync(Func<Food, bool> condition) => await AwaitableTask.RunAsync(() => GetWhere(condition));

        #endregion Async Shortcuts
    }
}