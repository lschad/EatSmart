using SchadLucas.EatSmart.Utilities;
using System.Windows;

namespace SchadLucas.EatSmart.UI

{
    public partial class Region : IRegion
    {
        public static readonly DependencyProperty RegionContentProperty = DpHelper.Register<Region, object>(nameof(RegionContent), RegionContentorContextChanged);
        public static readonly DependencyProperty RegionContextProperty = DpHelper.Register<Region, object>(nameof(RegionContext), RegionContentorContextChanged);
        public static readonly DependencyProperty RegionNameProperty = DpHelper.Register<Region, string>(nameof(RegionName), RegionNameChangedCallback);
        public static readonly DependencyProperty VisibleProperty = DpHelper.Register<Region, bool>(nameof(Visible));

        public object RegionContent
        {
            get => GetValue(RegionContentProperty);
            set => SetValue(RegionContentProperty, value);
        }

        public object RegionContext
        {
            get => GetValue(RegionContextProperty);
            set => SetValue(RegionContextProperty, value);
        }

        public string RegionName
        {
            get => (string)GetValue(RegionNameProperty);
            set => SetValue(RegionNameProperty, value);
        }

        public bool Visible
        {
            get => (bool)GetValue(VisibleProperty);
            set => SetValue(VisibleProperty, value);
        }

        public Region()
        {
            InitializeComponent();
            Visible = true;
        }

        private static void RegionContentorContextChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (d.GetValue(RegionContentProperty) is FrameworkElement view)
            {
                var dataContext = d.GetValue(RegionContextProperty);
                ViewModelBinder.BindView(dataContext, view);
            }
        }

        private static void RegionNameChangedCallback(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (d is Region region)
            {
                region.Name = $"{region.RegionName.Replace(".", "_")}";
            }
        }

        public override string ToString()
        {
            return string.IsNullOrEmpty(RegionName) ? base.ToString() : RegionName;
        }
    }
}