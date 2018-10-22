using SchadLucas.EatSmart.Data.EventArgs;
using SchadLucas.EatSmart.Data.Types;
using System;

namespace SchadLucas.EatSmart.Services
{
    public interface IPopupNotificationService : IService
    {
        event EventHandler<PopupNotifcationEventArgs> Published;

        void Publish(string notification);

        void Publish(IPopupNotification notification);
    }
}