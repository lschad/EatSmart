using SchadLucas.EatSmart.Data.Commands;
using SchadLucas.EatSmart.Data.Models;
using SchadLucas.EatSmart.Data.Types.Entities;
using SchadLucas.EatSmart.Services;
using SchadLucas.EatSmart.UI.Dialogs;
using SchadLucas.EatSmart.UI.Views;
using SchadLucas.EatSmart.ViewModels.Dialogs;
using System;
using System.Windows.Input;

namespace SchadLucas.EatSmart.ViewModels
{
    public sealed class FoodDetailViewModel : ViewModel, IFoodDetailViewModel
    {
        private ICommand _deleteCommand;
        private Food _food;

        private IFoodDetailModel Model { get; }
        private INavigationService NavigationService { get; }
        private IPopupNotificationService NotificationService { get; }
        private IApplicationOverlayService OverlayService { get; }

        public ICommand DeleteCommand => _deleteCommand ?? (_deleteCommand = new RelayCommand(Delete, CanDelete));
        public Food Food
        {
            get => _food;
            private set => SetField(ref _food, value);
        }

        public FoodDetailViewModel(IFoodDetailModel model, INavigationService navigationService, IPopupNotificationService notificationService, IApplicationOverlayService overlayService)
        {
            Model = model;
            NavigationService = navigationService;
            NotificationService = notificationService;
            OverlayService = overlayService;
        }

        private bool CanDelete(object obj) => Food != default;

        private void ClosedCallback(IFoodDetailDeleteDialog dialog, IFoodDetailDeleteDialogContext dialogContext)
        {
            // todo: fixyo
            //if (response == OkCancelDialogResponse.Ok)
            //{
            //    Model.Delete(Food.Id);

            //    var food = Food; // clone food to have it ready for the undo-callback
            //    var notification = new PopupNotification
            //    {
            //        Message = $"{Food.Name} deleted",
            //        Actions = new IPopupAction[]
            //        {
            //            new PopupAction(() =>
            //            {
            //                {
            //                    Model.Add(food);
            //                    NavigationService.NavigateTo<IFoodDetailView>(new object[] { food });
            //                }
            //            }, "Undo"),
            //        }
            //    };
            //    NotificationService.Publish(notification);
            //}

            OverlayService.CloseActiveOverlay();
            NavigationService.NavigateTo<IFoodView>();
            Food = default;
        }
        private void Delete(object obj)
        {
            OverlayService.OpenOverlay<IFoodDetailDeleteDialog, IFoodDetailDeleteDialogContext>(ClosedCallback);
        }
        public override void SetArguments<T>(T[] args)
        {
            base.SetArguments(args);
            if (args.Length == 1 && args[0] is Food food)
            {
                Food = food;
            }
            else
            {
                throw new ArgumentException("Only one argument is allowed", nameof(args));
            }
        }
    }
}