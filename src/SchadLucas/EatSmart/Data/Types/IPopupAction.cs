using System;
using System.Windows.Input;

namespace SchadLucas.EatSmart.Data.Types
{
    public interface IPopupAction
    {
        event EventHandler Executed;
        event EventHandler Executing;

        ICommand Action { get; }
        string Title { get; }
    }
}