using SchadLucas.EatSmart.Data.Types;

namespace SchadLucas.EatSmart.Data.EventArgs
{
    public class PopupNotifcationEventArgs : System.EventArgs
    {
        public IPopupNotification Notification { get; }

        public PopupNotifcationEventArgs(IPopupNotification notification)
        {
            Notification = notification;
        }
    }
}