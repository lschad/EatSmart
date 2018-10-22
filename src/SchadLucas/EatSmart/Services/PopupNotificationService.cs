using SchadLucas.EatSmart.Data.EventArgs;
using SchadLucas.EatSmart.Data.Types;
using System;

namespace SchadLucas.EatSmart.Services
{
    public sealed class PopupNotificationService : Service, IPopupNotificationService
    {
        public event EventHandler<PopupNotifcationEventArgs> Published;

        private IMessageHubService MessageHub { get; }

        public PopupNotificationService(IMessageHubService messageHub)
        {
            MessageHub = messageHub;
        }

        private void OnPublished(PopupNotifcationEventArgs e)
        {
            Published?.Invoke(this, e);
        }

        public void Publish(string notification) => Publish(new PopupNotification { Message = notification });

        public void Publish(IPopupNotification notification)
        {
            Logger.Debug($"Publish PopupNotification {notification}");
            MessageHub.Publish(notification);
            OnPublished(new PopupNotifcationEventArgs(notification));
        }
    }
}