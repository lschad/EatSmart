using SchadLucas.EatSmart.Data.Enums;
using SchadLucas.EatSmart.UI.Views;
using SchadLucas.EatSmart.ViewModels.Dialogs;

namespace SchadLucas.EatSmart.UI.Dialogs
{
    public interface IFoodDetailDeleteDialog : IView { }

    /// <summary>
    ///     Interaction logic for FoodDetailDeleteDialog.xaml
    /// </summary>
    public partial class FoodDetailDeleteDialog : IFoodDetailDeleteDialog
    {
        public FoodDetailDeleteDialog()
        {
            InitializeComponent();
        }

        private void OkCancelDialog_OnResponded(object sender, OkCancelDialogResponse e)
        {
            if (DataContext is IFoodDetailDeleteDialogContext ctx)
            {
                if (ctx.RespondCommand.CanExecute(e))
                {
                    ctx.RespondCommand.Execute(e);
                }
            }
        }
    }
}