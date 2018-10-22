using SchadLucas.EatSmart.UI.Views;
using SchadLucas.EatSmart.ViewModels;
using System.Windows;

namespace SchadLucas.EatSmart.Utilities
{
    public static class ViewModelBinder
    {
        private static void Bind(object viewModel, FrameworkElement view)
        {
            if (viewModel is null || view is null)
            {
                return;
            }

            if (viewModel.Equals(view.DataContext) == false)
            {
                view.DataContext = viewModel;
            }
        }

        public static void BindArguments<T>(IViewModel viewModel, T[] arguments)
        {
            viewModel.SetArguments(arguments);
        }

        public static void BindView(object viewModel, FrameworkElement view)
        {
            Bind(viewModel, view);
        }

        public static void BindView(IViewModel viewModel, IView view)
        {
            Bind(viewModel, view as FrameworkElement);
        }
    }
}