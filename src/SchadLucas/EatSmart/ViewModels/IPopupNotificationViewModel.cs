using SchadLucas.EatSmart.Data.Types;
using System.Collections.ObjectModel;

namespace SchadLucas.EatSmart.ViewModels
{
    public interface IPopupNotificationViewModel : IViewModel
    {
        ObservableCollection<IPopupNotification> Notifications { get; }
    }
}