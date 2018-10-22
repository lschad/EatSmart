using JetBrains.Annotations;
using SchadLucas.EatSmart.Data.Types.Entities;
using SchadLucas.EatSmart.Services;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SchadLucas.EatSmart.Data.Models
{
    [UsedImplicitly]
    public class FoodModel : Model, IFoodModel
    {
        private IFoodModelService Model { get; }

        // ReSharper disable once SuggestBaseTypeForParameter
        public FoodModel(IDbInMemoryFactoryService contextFactory, IFoodModelService model) : base(contextFactory)
        {
            Model = model;

            var foods = new List<Food>();
            for (var i = 0; i < 99; i++)
            {
                foods.Add(new Food { Name = $"Food {i}" });
            }

            Model.AddRange(foods.ToArray());
        }

        public void Add(Food food) => Model.Add(food);

        public async Task AddAsync(Food food) => await Model.AddAsync(food).ConfigureAwait(false);

        public bool Exists(string name) => Model.Exists(name);

        public async Task<bool> ExistsAsync(string name) => await Model.ExistsAsync(name);

        public IEnumerable<Food> Get() => Model.GetAll();

        public async Task<IEnumerable<Food>> GetAsync() => await Model.GetAllAsync();

        public IEnumerable<Food> GetWhere(Func<Food, bool> condition) => Model.GetWhere(condition);

        public bool Remove(Food food) => Model.Delete(food);

        public bool Remove(Guid id) => Model.Delete(id);
    }
}