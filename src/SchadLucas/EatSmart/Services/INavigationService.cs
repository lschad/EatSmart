using SchadLucas.EatSmart.Data.Types;
using SchadLucas.EatSmart.UI.Views;
using SchadLucas.EatSmart.ViewModels;
using System;

namespace SchadLucas.EatSmart.Services
{
    public interface INavigationService : IService
    {
        IHistoryItem Current { get; }
        bool HasNext { get; }
        bool HasPrevious { get; }
        IHistoryItem Next { get; }
        IHistoryItem Previous { get; }

        void NavigateBackward();

        void NavigateForward();

        void NavigateTo<TView, TViewModel>(object[] arguments = default)
            where TView : IView
            where TViewModel : IViewModel;

        void NavigateTo<TView>(object[] arguments = default);

        void NavigateTo(Type view, object[] arguments = default);
    }
}