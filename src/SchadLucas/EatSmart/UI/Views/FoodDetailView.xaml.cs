using JetBrains.Annotations;

namespace SchadLucas.EatSmart.UI.Views
{
    public interface IFoodDetailView : IView { }

    /// <summary>
    ///     Interaction logic for FoodDetailView.xaml
    /// </summary>
    [UsedImplicitly]
    public partial class FoodDetailView : IFoodDetailView
    {
        public FoodDetailView()
        {
            InitializeComponent();
        }
    }
}