using SchadLucas.EatSmart.Data.EventArgs;
using SchadLucas.EatSmart.Data.Types;
using SchadLucas.EatSmart.Data.Types.Collections;
using SchadLucas.EatSmart.Services;
using SchadLucas.EatSmart.Utilities;
using System;
using System.Collections;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Threading;

namespace SchadLucas.EatSmart.ViewModels
{
    public sealed class PopupNotificationViewModel : ViewModel, IPopupNotificationViewModel
    {
        private ObservableCollection<IPopupNotification> _notifications;

        public ObservableCollection<IPopupNotification> Notifications => _notifications ?? (_notifications = new ObservableDisposableObjectCollection<IPopupNotification>());

        public PopupNotificationViewModel(IPopupNotificationService notificationService)
        {
            Notifications.CollectionChanged += OnCollectionChanged;
            notificationService.Published += OnNotificationPublished;
        }

        private void Close(object obj)
        {
            if (obj is IPopupNotification notification)
            {
                if (Notifications.Contains(notification))
                {
                    Logger.Debug($"Close PopupNotification: {notification}");

                    UiDispatcher.Invoke(() => Notifications.Remove(notification));
                }
            }
        }

        private void CloseNotification(IPopupNotification notification, Action<IPopupNotification> callback)
        {
            Thread.Sleep(notification.Duration);
            //if (notification.MouseOver()) // todo: fix mouseover
            if (false)
            {
                CloseNotification(notification, callback);
            }
            else
            {
                callback(notification);
            }
        }

        private void DelayedRemoval(IPopupNotification notification)
        {
            AwaitableTask.Run(() => CloseNotification(notification, Close));
        }

        private void OnCollectionAdded(IEnumerable items)
        {
            foreach (IPopupNotification notification in items)
            {
                Logger.Debug($"Added PopupNotification: {notification}");

                if (notification.Sticky == false)
                {
                    DelayedRemoval(notification);
                }
            }
        }

        private void OnCollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            if (e.Action == NotifyCollectionChangedAction.Add)
            {
                OnCollectionAdded(e.NewItems);
            }
        }

        private void OnNotificationPublished(object sender, PopupNotifcationEventArgs e)
        {
            Notifications.Add(e.Notification);
        }
    }
}