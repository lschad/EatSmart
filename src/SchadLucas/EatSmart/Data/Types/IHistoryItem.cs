using SchadLucas.EatSmart.UI.Views;
using SchadLucas.EatSmart.ViewModels;
using System;

namespace SchadLucas.EatSmart.Data.Types
{
    public interface IHistoryItem
    {
        object[] Arguments { get; }
        Guid Id { get; }
        IView View { get; }
        IViewModel ViewModel { get; }
    }
}