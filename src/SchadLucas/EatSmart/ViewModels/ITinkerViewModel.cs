using SchadLucas.EatSmart.Data.Commands;
using SchadLucas.EatSmart.Services;
using SchadLucas.EatSmart.UI.Dialogs;
using SchadLucas.EatSmart.ViewModels.Dialogs;
using System.Windows.Input;

namespace SchadLucas.EatSmart.ViewModels
{
    public interface ITinkerViewModel : IViewModel
    {
        ICommand OpenOverlayCommand { get; }
    }

    public class TinkerViewModel : ViewModel, ITinkerViewModel
    {
        private readonly IApplicationOverlayService _overlayService;

        private ICommand _openOverlayCommand;
        public ICommand OpenOverlayCommand => _openOverlayCommand ?? (_openOverlayCommand = new RelayCommand(OpenOverlay));

        public TinkerViewModel(IApplicationOverlayService overlayService)
        {
            _overlayService = overlayService;
        }

        private void OpenOverlay(object obj)
        {
            _overlayService.OpenOverlay<ITinkerDialog, ITinkerDialogContext>((view, viewModel) =>
            {
                Logger.Fatal($"OVERLAY CLOSED LUL (view:{view}; viewmodel:{viewModel})");
            });
        }
    }
}