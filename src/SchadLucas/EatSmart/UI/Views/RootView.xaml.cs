namespace SchadLucas.EatSmart.UI.Views
{
    public interface IRootView : IView { }

    /// <summary>
    ///     Interaction logic for RootView.xaml
    /// </summary>
    public partial class RootView : IRootView
    {
        public RootView()
        {
            InitializeComponent();
        }
    }
}