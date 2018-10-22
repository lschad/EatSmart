using Autofac;
using SchadLucas.EatSmart.UI.Views;
using SchadLucas.EatSmart.ViewModels;
using System;
using System.Linq;
using System.Reflection;

namespace SchadLucas.EatSmart.Services
{
    public class ViewModelHelper : Service, IViewModelHelper
    {
        private ILifetimeScope Container { get; }

        public ViewModelHelper(ILifetimeScope container)
        {
            Container = container;
        }

        private object Resolve(Type type)
        {
            Container.TryResolve(type, out var result);
            return result;
        }

        public IView GetViewFromType(Type type)
        {
            return Resolve(type) as IView;
        }

        public IViewModel GetViewModelFromType(Type type)
        {
            return Resolve(type) as IViewModel;
        }

        public Type GetViewModelTypeFromViewType(Type viewType)
        {
            if (!viewType.IsAssignableTo<IView>())
            {
                throw new ArgumentException("Type is not assignable to IView", nameof(viewType));
            }

            var name = viewType.Name.Replace("View", "ViewModel"); // todo: ugh.
            return Assembly
                   .GetExecutingAssembly() // todo get assembly from central place
                   .ExportedTypes
                   .FirstOrDefault(t => t.Name == name);
        }
    }
}