using System.Collections.Generic;
using System.Linq;
using Autofac;
using Autofac.Core;
using NLog;

namespace SchadLucas.EatSmart.Modules
{
    public class LogEventModule : Module
    {
        private static Logger Logger => LogManager.GetLogger("Autofac");

        private string FormatServices(IEnumerable<Service> services) => string.Join(", ", services.Select(s => $"{s.Description}"));

        private string BuildString(string verb, object instance, IEnumerable<Service> services) => $"{verb} <{instance}> as {FormatServices(services)}";

        private void Log(string verb, object instance, IEnumerable<Service> services) => Logger.Trace(BuildString(verb, instance, services));

        protected override void Load(ContainerBuilder builder)
        {
            base.Load(builder);
            builder.RegisterCallback(componentRegistry =>
            {
                componentRegistry.Registered += (sender, e) =>
                {
                    var registration = e.ComponentRegistration;

                    registration.Preparing += (s, t) =>
                        Log("Preparing", t.Component.Activator.LimitType.Name, t.Component.Services);

                    registration.Activating += (s, t) =>
                        Log("Activating", t.Instance.GetType().Name, t.Component.Services);

                    registration.Activated += (s, t) =>
                        Log("Activated", t.Instance.GetType().Name, t.Component.Services);
                };
            });
            builder.RegisterBuildCallback(container =>
            {
                container.ComponentRegistry.Registered += (s, t) =>
                    Log("Registered", t.ComponentRegistration.Activator.LimitType.Name, t.ComponentRegistration.Services);
            });
        }
    }
}