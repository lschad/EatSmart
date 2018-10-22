using SchadLucas.EatSmart.Data.Types.Entities;
using SchadLucas.EatSmart.Services;
using System;
using System.Threading.Tasks;

namespace SchadLucas.EatSmart.Data.Models
{
    public class FoodDetailModel : Model, IFoodDetailModel
    {
        private IFoodModelService FoodModel { get; }

        public FoodDetailModel(IDbInMemoryFactoryService dbFactory, IFoodModelService foodModel) : base(dbFactory)
        {
            FoodModel = foodModel;
        }

        public void Add(Food food) => FoodModel.Add(food);

        public bool Delete(Guid id) => FoodModel.Delete(id);

        public async Task<bool> DeleteAsync(Guid id) => await FoodModel.DeleteAsync(id).ConfigureAwait(false);

        public async Task<bool> DeleteAsync(Food food) => await FoodModel.DeleteAsync(food).ConfigureAwait(false);
    }
}