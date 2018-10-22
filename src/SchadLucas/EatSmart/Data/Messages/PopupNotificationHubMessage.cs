using SchadLucas.EatSmart.Data.Types;

namespace SchadLucas.EatSmart.Data.Messages
{
    public class PopupNotificationHubMessage : HubMessage
    {
        public IPopupNotification Notification { get; }

        public PopupNotificationHubMessage(object sender, IPopupNotification notification) : base(sender)
        {
            Notification = notification;
        }
    }
}