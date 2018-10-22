using SchadLucas.EatSmart.ViewModels;
using System;

namespace SchadLucas.EatSmart.Data.Types
{
    public interface INavigationElement
    {
        Guid Id { get; }

        string Title { get; }

        object View { get; }

        IViewModel ViewModel { get; }
    }
}