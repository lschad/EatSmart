using System.Reflection;
using Autofac;
using SchadLucas.EatSmart.ViewModels;

namespace SchadLucas.EatSmart.Modules
{
    public class ViewModelModule : ModuleBase
    {
        public ViewModelModule(Assembly[] assemblies) : base(assemblies)
        {
        }

        protected override void Load(ContainerBuilder builder)
        {
            base.Load(builder);
            RegisterAsSingleton<IViewModel>(builder);
        }
    }
}