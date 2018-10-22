using System.Windows.Input;

namespace SchadLucas.EatSmart.ViewModels
{
    public interface INavigationViewModel : IViewModel
    {
        bool CanNavigateBackward { get; }
        bool CanNavigateForward { get; }
        ICommand NavigateBackwardCommand { get; }
        ICommand NavigateForwardCommand { get; }
        ICommand NavigateToCommand { get; }
    }
}