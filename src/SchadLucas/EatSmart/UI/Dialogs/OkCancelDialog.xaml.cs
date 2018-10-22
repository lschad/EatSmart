using SchadLucas.EatSmart.Utilities;
using System.Windows;
using System.Windows.Input;
using SchadLucas.EatSmart.Data.Enums;

namespace SchadLucas.EatSmart.UI.Dialogs
{
    /// <summary>
    ///     Interaction logic for OkCancelDialog.xaml
    /// </summary>
    public partial class OkCancelDialog
    {
        public static readonly DependencyProperty DialogTextProperty = DpHelper.Register<OkCancelDialog, string>(nameof(DialogText));
        public static readonly DependencyProperty DialogTitleProperty = DpHelper.Register<OkCancelDialog, string>(nameof(DialogTitle));
        public static readonly DependencyProperty DialogCallbackProperty = DpHelper.Register<OkCancelDialog, ICommand>(nameof(DialogCallback));

        public string DialogText
        {
            get => GetValue(DialogTextProperty).ToString();
            set => SetValue(DialogTextProperty, value);
        }
        public string DialogTitle
        {
            get => GetValue(DialogTitleProperty).ToString();
            set => SetValue(DialogTitleProperty, value);
        }
        public ICommand DialogCallback
        {
            get => (ICommand)GetValue(DialogCallbackProperty);
            set => SetValue(DialogCallbackProperty, value);
        }

        public OkCancelDialog()
        {
            InitializeComponent();
        }

        private void OnOkClick(object sender, RoutedEventArgs e)
        {
            OnClick(OkCancelDialogResponse.Ok);
        }

        private void OnCancelClick(object sender, RoutedEventArgs e)
        {
            OnClick(OkCancelDialogResponse.Cancel);
        }

        private void OnClick(OkCancelDialogResponse response)
        {
            if (DialogCallback != null && DialogCallback.CanExecute(response))
            {
                if (DialogCallback.CanExecute(response))
                {
                    DialogCallback.Execute(response);
                }
            }
        }
    }
}