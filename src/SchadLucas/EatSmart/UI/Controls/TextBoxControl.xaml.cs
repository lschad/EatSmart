using SchadLucas.EatSmart.Utilities;
using System;
using System.Windows;
using System.Windows.Media.Animation;

namespace SchadLucas.EatSmart.UI.Controls
{
    /// <summary>
    ///     Interaction logic for TextBoxControl.xaml
    /// </summary>
    /// <inheritdoc cref="System.Windows.Controls.UserControl" />
    public partial class TextBoxControl
    {
        private const double EmphasizedLabelFontSize = 14;
        private const double EmphasizedLabelMargin = 20;
        private const double LabelFontSize = 12;
        private const double LabelMargin = 0;
        private static readonly Duration AnimationDuration = new Duration(TimeSpan.FromSeconds(.15f));

        public static readonly DependencyProperty LabelProperty = DpHelper.Register<TextBoxControl>(nameof(Label));
        public static readonly DependencyProperty TextProperty = DpHelper.Register<TextBoxControl>(nameof(Text));

        public object Label
        {
            get => GetValue(LabelProperty);
            set => SetValue(LabelProperty, value);
        }
        public object Text
        {
            get => GetValue(TextProperty);
            set => SetValue(TextProperty, value);
        }

        public TextBoxControl()
        {
            InitializeComponent();
            TextBox.TextChanged += (s, e) => Text = TextBox.Text;
        }

        private void UpdateLabel(object sender = null, EventArgs e = null)
        {
            if (TextBox == null || TextBlock == null)
            {
                return;
            }

            double newMargin;
            var originalMargin = TextBlock.Margin.Top;

            double newFontSize;
            var originalFontSize = TextBlock.FontSize;

            if (TextBox.IsFocused)
            {
                newMargin = LabelMargin;
                newFontSize = LabelFontSize;
            }
            else
            {
                if (TextBox.Text == string.Empty)
                {
                    newMargin = EmphasizedLabelMargin;
                    newFontSize = EmphasizedLabelFontSize;
                }
                else
                {
                    newMargin = LabelMargin;
                    newFontSize = LabelFontSize;
                }
            }

            TextBlock.BeginAnimation(MarginProperty, new ThicknessAnimation
            {
                From = new Thickness(0, originalMargin, 0, 0),
                To = new Thickness(0, newMargin, 0, 0),
                Duration = AnimationDuration
            });
            TextBlock.BeginAnimation(FontSizeProperty, new DoubleAnimation
            {
                From = originalFontSize,
                To = newFontSize,
                Duration = AnimationDuration
            });
        }
    }
}