using SchadLucas.EatSmart.Data.Enums;
using System.Windows.Input;

namespace SchadLucas.EatSmart.ViewModels.Dialogs
{
    public interface IFoodDetailDeleteDialogContext : IApplicationOverlayDialog
    {
        ICommand RespondCommand { get; }
        OkCancelDialogResponse Response { get; }
    }
}