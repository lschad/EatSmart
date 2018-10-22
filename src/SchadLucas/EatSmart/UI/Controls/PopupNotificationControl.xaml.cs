using SchadLucas.EatSmart.Data.Enums;
using SchadLucas.EatSmart.Data.Types;
using SchadLucas.EatSmart.Utilities;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;

namespace SchadLucas.EatSmart.UI.Controls
{
    public partial class PopupNotificationControl
    {
        private object _closeContext;
        public static readonly DependencyProperty ActionsProperty = DpHelper.Register<PopupNotificationControl, IEnumerable<IPopupAction>>(nameof(Actions));
        public static readonly DependencyProperty NotificationLevelProperty = DpHelper.Register<PopupNotificationControl, PopupNotificationLevel>(nameof(NotificationLevel));
        public static readonly DependencyProperty TextProperty = DpHelper.Register<PopupNotificationControl, string>(nameof(Text));

        public IEnumerable<IPopupAction> Actions
        {
            get => (IEnumerable<IPopupAction>)GetValue(ActionsProperty);
            set => SetValue(ActionsProperty, value);
        }
        public object CloseContext => _closeContext ?? (_closeContext = new PopupAction(Close, "X"));
        /// <remarks>Default value is set to <see cref="PopupNotificationLevel.Info" /></remarks>
        public PopupNotificationLevel NotificationLevel
        {
            get
            {
                if (GetValue(NotificationLevelProperty) is PopupNotificationLevel level)
                {
                    return level;
                }
                else
                {
                    return PopupNotificationLevel.Info;
                }
            }
            set => SetValue(NotificationLevelProperty, value);
        }
        public string Text
        {
            get => (string)GetValue(TextProperty);
            set => SetValue(TextProperty, value);
        }

        public PopupNotificationControl()
        {
            InitializeComponent();
        }
        private void Close()
        {
            if (DataContext is IPopupNotification dc)
            {
                dc.Dispose();
            }

            if (Parent is Grid p)
            {
                p.Children.Remove(this);
            }
        }
    }
}