using SchadLucas.EatSmart.Data.Types.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SchadLucas.EatSmart.Data.Models
{
    public interface IFoodModel : IModel
    {
        void Add(Food food);

        Task AddAsync(Food food);

        bool Exists(string name);

        Task<bool> ExistsAsync(string name);

        IEnumerable<Food> Get();

        Task<IEnumerable<Food>> GetAsync();

        IEnumerable<Food> GetWhere(Func<Food, bool> condition);

        bool Remove(Food food);

        bool Remove(Guid id);
    }
}