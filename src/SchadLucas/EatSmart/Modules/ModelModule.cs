using System.Reflection;
using Autofac;
using SchadLucas.EatSmart.Data.Models;

namespace SchadLucas.EatSmart.Modules
{
    public class ModelModule : ModuleBase
    {
        public ModelModule(Assembly[] assemblies) : base(assemblies)
        {
        }

        protected override void Load(ContainerBuilder builder)
        {
            base.Load(builder);
            RegisterAsSingleton<IModel>(builder);
        }
    }
}