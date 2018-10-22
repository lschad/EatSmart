namespace SchadLucas.EatSmart.ViewModels.Dialogs
{
    public interface ITinkerDialogContext : IApplicationOverlayDialog { }

    public class TinkerDialogContext : ApplicationOverlayDialog, ITinkerDialogContext
    {
    }
}