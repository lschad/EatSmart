namespace SchadLucas.EatSmart.UI.Views
{
    public interface INavigationView : IView { }

    /// <summary>
    ///     Interaction logic for NavigationControl.xaml
    /// </summary>
    public partial class NavigationView : INavigationView
    {
        public NavigationView()
        {
            InitializeComponent();
        }
    }
}