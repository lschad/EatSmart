using SchadLucas.EatSmart.UI.Views;

namespace SchadLucas.EatSmart.UI.Dialogs
{
    public interface ITinkerDialog : IView { }

    public partial class TinkerDialog : ITinkerDialog
    {
        public TinkerDialog()
        {
            InitializeComponent();
        }
    }
}