using SchadLucas.EatSmart.Data.Types.Entities;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace SchadLucas.EatSmart.ViewModels
{
    public interface IFoodViewModel : IViewModel
    {
        ICommand AddFoodCommand { get; }
        ObservableCollection<Food> Foods { get; set; }
        bool IsLoading { get; set; }
        string NewFoodText { get; set; }
        ICommand OpenFoodCommand { get; }
        ICommand SearchFoodCommand { get; }
        string SearchText { get; set; }
        Food SelectedFood { get; set; }
    }
}