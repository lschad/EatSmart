using SchadLucas.EatSmart.UI.Views;
using SchadLucas.EatSmart.ViewModels;
using System;

namespace SchadLucas.EatSmart.Data.Types
{
    public class HistoryItem : IHistoryItem
    {
        private static int _sequence;
        private readonly int _i;

        public object[] Arguments { get; }
        public Guid Id { get; }
        public IView View { get; }
        public IViewModel ViewModel { get; }

        public HistoryItem(IView view, IViewModel viewModel, object[] arguments)
        {
            _i = _sequence + 0;
            _sequence++;
            View = view;
            ViewModel = viewModel;
            Arguments = arguments;
            Id = Guid.NewGuid();
        }

        public override string ToString()
        {
            return $"HistoryItem {_i}";
        }
    }
}