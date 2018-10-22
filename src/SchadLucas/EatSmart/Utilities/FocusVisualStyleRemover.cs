using System.Windows;

namespace SchadLucas.EatSmart.Utilities
{
    public static class FocusVisualStyleRemover
    {
        private static RoutedEventHandler RemoveFocusVisualStyle => (s, e) =>
        {
            if (s is FrameworkElement el)
            {
                el.FocusVisualStyle = null;
            }
        };

        public static void Init()
        {
            EventManager.RegisterClassHandler(typeof(FrameworkElement), UIElement.GotFocusEvent, RemoveFocusVisualStyle, true);
        }
    }
}