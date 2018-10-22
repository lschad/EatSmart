using System.Reflection;
using Autofac;
using SchadLucas.EatSmart.UI.Views;

namespace SchadLucas.EatSmart.Modules
{
    public class ViewModule : ModuleBase
    {
        public ViewModule(Assembly[] assemblies) : base(assemblies)
        {
        }

        protected override void Load(ContainerBuilder builder)
        {
            base.Load(builder);
            RegisterAsSingleton<IView>(builder);
        }
    }
}