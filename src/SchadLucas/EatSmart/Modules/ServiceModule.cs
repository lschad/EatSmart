using System.Reflection;
using Autofac;
using SchadLucas.EatSmart.Services;

namespace SchadLucas.EatSmart.Modules
{
    public class ServiceModule : ModuleBase
    {
        public ServiceModule(Assembly[] assemblies) : base(assemblies)
        {
        }

        protected override void Load(ContainerBuilder builder)
        {
            base.Load(builder);
            RegisterAsSingleton<IService>(builder);
        }
    }
}