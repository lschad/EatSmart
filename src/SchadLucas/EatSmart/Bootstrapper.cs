using Autofac;
using NLog.Config;
using NLog.Targets;
using SchadLucas.EatSmart.Modules;
using SchadLucas.EatSmart.Regions;
using SchadLucas.EatSmart.UI;
using SchadLucas.EatSmart.UI.Views;
using SchadLucas.EatSmart.ViewModels;
using System.Diagnostics;
using System.Reflection;
using System.Windows;

namespace SchadLucas.EatSmart
{
    public sealed class Bootstrapper : BootstrapperBase
    {
        private readonly Assembly[] _assemblies = { Assembly.GetExecutingAssembly() };
        private readonly ApplicationLoadingScreen _loadingScreen = new ApplicationLoadingScreen(new LoadingScreenView());

        public Bootstrapper()
        {
            _loadingScreen.Show();
            Initialize();
        }

        private void RepositionWindow()
        {
            // todo: calc mid of loadingscreen window and set mainwindow to the same center.
            Window.Left = _loadingScreen.Left;
            Window.Top = _loadingScreen.Top;
        }

        protected override void BuildRegions(IRegionBuilder regionBuilder)
        {
            base.BuildRegions(regionBuilder);
            regionBuilder.BuildRegion<NavigationRegion>();
            regionBuilder.BuildRegion<PopupNotificationRegion>();
            regionBuilder.BuildRegion<ApplicationOverlayRegion>();
        }

        protected override LoggingConfiguration GetLoggingConfiguration()
        {
            var cfg = base.GetLoggingConfiguration();

            var targetDevelopment = new DebuggerTarget
            {
                Layout = "${pad:padding=-30:inner=[${level:format=FirstCharacter}\\:${logger}]} ${message} ${onexception:inner=${newline}${exception:format=toString,Data:maxInnerExceptionLevel=10}}",
                Name = "Developer Log"
            };
            var targetChainsaw = new ChainsawTarget("ChainsawTarget")
            {
                Layout = "${longdate}|${level:uppercase=true}|${logger}|${message}",
                Address = "udp://localhost:4000"
            };

            cfg.AddTarget(targetDevelopment);
            cfg.AddTarget(targetChainsaw);
            cfg.AddRuleForAllLevels(targetDevelopment);
            cfg.AddRuleForAllLevels(targetChainsaw);

            return cfg;
        }

        protected override IViewModel GetRootDataContext()
        {
            return Container.Resolve<IRootViewModel>();
        }

        protected override Window GetRootWindow()
        {
            return Container.Resolve<IRootView>() as Window;
        }

        protected override void InitializeLogging()
        {
            base.InitializeLogging();

            PresentationTraceSources.Refresh();
            foreach (var p in typeof(PresentationTraceSources).GetProperties(BindingFlags.Public | BindingFlags.Static))
            {
                if (p.GetValue(null) is TraceSource src)
                {
                    src.Switch.Level = SourceLevels.All;
                    src.Listeners.Add(new LogPresentationSourceListener(p.Name));
                }
            }
        }

        protected override void OnWindowLoaded(object sender, RoutedEventArgs e)
        {
            RepositionWindow();
            _loadingScreen.Close();
            base.OnWindowLoaded(sender, e);
        }

        protected override void RegisterModules(ContainerBuilder builder)
        {
            base.RegisterModules(builder);
            builder.RegisterModule<LogEventModule>();
            builder.RegisterModule(new ServiceModule(_assemblies));
            builder.RegisterModule(new ModelModule(_assemblies));
            builder.RegisterModule(new ViewModelModule(_assemblies));
            builder.RegisterModule(new ViewModule(_assemblies));
        }
    }
}