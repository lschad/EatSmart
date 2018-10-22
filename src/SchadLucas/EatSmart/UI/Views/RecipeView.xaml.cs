namespace SchadLucas.EatSmart.UI.Views
{
    public interface IRecipeView : IView
    {
    }

    /// <summary>
    ///     Interaction logic for RecipeView.xaml
    /// </summary>
    public partial class RecipeView : IRecipeView
    {
        public RecipeView()
        {
            InitializeComponent();
        }
    }
}