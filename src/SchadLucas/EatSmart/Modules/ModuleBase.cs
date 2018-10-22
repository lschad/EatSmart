using System.Linq;
using System.Reflection;
using Autofac;

namespace SchadLucas.EatSmart.Modules
{
    public abstract class ModuleBase : Autofac.Module
    {
        protected Assembly[] Assemblies { get; }

        protected ModuleBase(Assembly[] assemblies)
        {
            Assemblies = assemblies;
        }

        protected virtual void RegisterAsSingleton<T>(ContainerBuilder builder, string @namespace = null)
        {
            if (string.IsNullOrEmpty(@namespace))
            {
                @namespace = $"{nameof(SchadLucas)}.{nameof(EatSmart)}";
            }

            builder
                .RegisterAssemblyTypes(Assemblies)
                .AssignableTo<T>()
                .As(type =>
                {
                    return type.GetInterfaces()
                            .Where(x => x.IsInNamespace(@namespace))
                            .Where(x => x != typeof(T));
                })
                .SingleInstance();
        }
    }
}