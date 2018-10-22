using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace SchadLucas.EatSmart.UI.Views
{
    public interface IPopupNotificationView : IView { }

    public partial class PopupNotificationView : IPopupNotificationView
    {
        public PopupNotificationView()
        {
            InitializeComponent();
        }

        private void OnPreviewMouseWheel(object sender, MouseWheelEventArgs e)
        {
            // disable scrolling.
            if (sender is ListBox box && !e.Handled)
            {
                e.Handled = true;
                var eventArg = new MouseWheelEventArgs(e.MouseDevice, e.Timestamp, e.Delta)
                {
                    RoutedEvent = MouseWheelEvent,
                    Source = box
                };
                var parent = box.Parent as UIElement;
                parent?.RaiseEvent(eventArg);
            }
        }
    }
}