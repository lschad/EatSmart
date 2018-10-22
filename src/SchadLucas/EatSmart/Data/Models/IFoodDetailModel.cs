using SchadLucas.EatSmart.Data.Types.Entities;
using System;
using System.Threading.Tasks;

namespace SchadLucas.EatSmart.Data.Models
{
    public interface IFoodDetailModel : IModel
    {
        void Add(Food food);

        bool Delete(Guid id);

        Task<bool> DeleteAsync(Guid id);

        Task<bool> DeleteAsync(Food food);
    }
}