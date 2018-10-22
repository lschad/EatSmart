using SchadLucas.EatSmart.Data.Commands;
using SchadLucas.EatSmart.UI.Views;
using SchadLucas.EatSmart.ViewModels;
using SchadLucas.EatSmart.ViewModels.Dialogs;
using System;
using System.Windows.Input;

namespace SchadLucas.EatSmart.UI.Dialogs
{
    #region DialogBase

    public interface IDialogBase : IView
    {
        IView InnerContent { get; set; }
    }
    public partial class DialogBase : IDialogBase
    {
        public IView InnerContent
        {
            get => (IView)WrappedContent.Content;
            set => WrappedContent.Content = value;
        }

        public DialogBase()
        {
            InitializeComponent();
        }
    }

    #endregion DialogBase

    #region ViewModel

    public interface IDialogBaseViewModel : IApplicationOverlayDialog
    {
        ICommand CloseCommand { get; }
    }

    public class DialogBaseViewModel : ViewModel, IDialogBaseViewModel
    {
        private ICommand _closeCommand;
        public event EventHandler Closed;
        public ICommand CloseCommand => _closeCommand ?? (_closeCommand = new RelayCommand(p =>
                {
                    OnClosed();
                }));

        private void OnClosed()
        {
            Closed?.Invoke(this, EventArgs.Empty);
        }
        public void Close()
        {
            throw new NotImplementedException();
        }
    }

    #endregion ViewModel
}