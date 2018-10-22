using SchadLucas.EatSmart.Data.Types.Entities;
using System.Windows.Input;

namespace SchadLucas.EatSmart.ViewModels
{
    public interface IFoodDetailViewModel : IViewModel
    {
        ICommand DeleteCommand { get; }
        Food Food { get; }
    }
}