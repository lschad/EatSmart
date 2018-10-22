using SchadLucas.EatSmart.ViewModels;
using System;
using JetBrains.Annotations;

namespace SchadLucas.EatSmart.Data.Types
{
    [UsedImplicitly]
    public class NavigationElement : INavigationElement
    {
        public Guid Id { get; }

        public string Title { get; }

        public object View { get; }

        public IViewModel ViewModel { get; }

        public NavigationElement(string title, object view, IViewModel viewModel)
        {
            Id = Guid.NewGuid();
            Title = title;
            View = view;
            ViewModel = viewModel;
        }

        public override string ToString() => Title;
    }
}