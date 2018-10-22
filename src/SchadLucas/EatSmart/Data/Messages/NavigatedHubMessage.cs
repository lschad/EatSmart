using SchadLucas.EatSmart.UI.Views;
using SchadLucas.EatSmart.ViewModels;

namespace SchadLucas.EatSmart.Data.Messages
{
    public class NavigatedHubMessage
    {
        public object[] Arguments { get; }

        public object Sender { get; }

        public IView View { get; }

        public IViewModel ViewModel { get; }

        public NavigatedHubMessage(object sender, IView view, IViewModel viewModel, object[] arguments)
        {
            Sender = sender;
            View = view;
            ViewModel = viewModel;
            Arguments = arguments;
        }
    }
}