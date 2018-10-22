using SchadLucas.EatSmart.Data.Commands;
using SchadLucas.EatSmart.Data.Enums;
using System.Windows.Input;

namespace SchadLucas.EatSmart.ViewModels.Dialogs
{
    public sealed class FoodDetailDeleteDialogContext : ApplicationOverlayDialog, IFoodDetailDeleteDialogContext
    {
        private ICommand _respondCommand;
        private OkCancelDialogResponse _response;

        public ICommand RespondCommand => _respondCommand ?? (_respondCommand = new RelayCommand(Respond));
        public OkCancelDialogResponse Response
        {
            get => _response;
            private set => SetField(ref _response, value);
        }
        public FoodDetailDeleteDialogContext()
        {
            Response = OkCancelDialogResponse.Cancel;
        }

        private void Respond(object obj)
        {
            if (obj is OkCancelDialogResponse response)
            {
                Response = response;
                OnClosed(response);
            }
        }
    }
}