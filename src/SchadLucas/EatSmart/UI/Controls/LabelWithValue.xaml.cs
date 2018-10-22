using System.Windows;
using JetBrains.Annotations;

namespace SchadLucas.EatSmart.UI.Controls
{
    /// <summary>
    ///     Interaction logic for LabelWithValue.xaml
    /// </summary>
    [UsedImplicitly]
    public partial class LabelWithValue
    {
        public static readonly DependencyProperty LabelProperty = DependencyProperty.Register(nameof(Label), typeof(object), typeof(LabelWithValue), new PropertyMetadata(null));
        public static readonly DependencyProperty SuffixProperty = DependencyProperty.Register(nameof(Suffix), typeof(object), typeof(LabelWithValue), new PropertyMetadata(null));
        public static readonly DependencyProperty TextProperty = DependencyProperty.Register(nameof(Text), typeof(object), typeof(LabelWithValue), new PropertyMetadata(null));

        public object Label
        {
            get => (string)GetValue(LabelProperty);
            set => SetValue(LabelProperty, value.ToString());
        }

        public object Suffix
        {
            get => (string)GetValue(SuffixProperty);
            set => SetValue(SuffixProperty, value.ToString());
        }

        public object Text
        {
            get => (string)GetValue(TextProperty);
            set => SetValue(TextProperty, value.ToString());
        }

        public LabelWithValue()
        {
            InitializeComponent();
        }
    }
}