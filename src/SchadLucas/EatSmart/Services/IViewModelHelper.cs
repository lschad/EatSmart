using System;
using SchadLucas.EatSmart.UI.Views;
using SchadLucas.EatSmart.ViewModels;

namespace SchadLucas.EatSmart.Services
{
    public interface IViewModelHelper : IService
    {
        IView GetViewFromType(Type type);

        IViewModel GetViewModelFromType(Type type);

        Type GetViewModelTypeFromViewType(Type viewType);
    }
}