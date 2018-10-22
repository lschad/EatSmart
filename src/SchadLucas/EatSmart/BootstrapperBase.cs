using Autofac;
using Autofac.Core;
using Autofac.Core.Registration;
using NLog;
using NLog.Config;
using SchadLucas.EatSmart.Regions;
using SchadLucas.EatSmart.Services;
using SchadLucas.EatSmart.UI;
using SchadLucas.EatSmart.Utilities;
using SchadLucas.EatSmart.ViewModels;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Reflection;
using System.Windows;

namespace SchadLucas.EatSmart
{
    [SuppressMessage("ReSharper", "VirtualMemberNeverOverridden.Global")]
    public abstract class BootstrapperBase
    {
        protected Application App { get; private set; }
        protected IContainer Container { get; private set; }
        protected Logger Logger => LogManager.GetLogger(GetType().Name);
        protected IRegionManager RegionManager { get; private set; }
        protected Window Window { get; private set; }

        private static IEnumerable<DependencyObject> DependencyObjectTraversal(DependencyObject obj)
        {
            var children = LogicalTreeHelper.GetChildren(obj);
            var dependencyObjects = children.OfType<DependencyObject>();

            return dependencyObjects.Reverse().ToList();
        }

        private void InitializeApp(Application app)
        {
            App = app ?? Application.Current;
            App.ShutdownMode = ShutdownMode.OnMainWindowClose;
            App.MainWindow = new Window(); // this succs. it's because of the LoadingScreen. I dont get it

            App.Startup += OnStartup;
            App.Exit += OnExit;
        }

        private void RegisterRegions(DependencyObject root)
        {
            var children = TreeTraversal.DepthFirst(root, DependencyObjectTraversal);
            var regions = children.OfType<Region>().ToList();

            regions.ForEach(RegionManager.Register);
        }

        protected virtual IContainer BuildIocContainer()
        {
            var builder = GetContainerBuilder();

            OnRegistering();
            RegisterModules(builder);
            OnRegistered();

            return builder.Build();
        }

        protected virtual void BuildRegions(IRegionBuilder regionBuilder)
        {
        }

        protected virtual void ConfigureMessageHub()
        {
            var messageHub = Container.Resolve<IMessageHubService>();
            messageHub.OnError += (token, e) =>
                Logger.Warn(e.Exception, $"Error thrown by the Messagehub. (token: {token})");
            messageHub.RegisterGlobalHandler((type, @event) =>
                Logger.Debug($"MessageHub Event: Type: {type}, Event: {@event}"));
        }

        protected virtual void ConfigureServices()
        {
            ConfigureMessageHub();
        }

        protected virtual ContainerBuilder GetContainerBuilder()
        {
            var builder = new ContainerBuilder();
            return builder;
        }

        protected virtual LoggingConfiguration GetLoggingConfiguration() => new LoggingConfiguration();

        protected abstract IViewModel GetRootDataContext();

        protected abstract Window GetRootWindow();

        protected virtual void Initialize(Application app = null)
        {
            OnInitializing();

            InitializeLogging();
            InitializeApp(app);

            OnInitialized();
        }

        protected virtual void InitializeLogging()
        {
            LogManager.Configuration = GetLoggingConfiguration();

            AppDomain.CurrentDomain.UnhandledException += (s, e) =>
                Logger.Fatal((Exception)e.ExceptionObject, "Unhandled Exception!");
            Application.Current.DispatcherUnhandledException +=
                (s, e) => Logger.Fatal(e.Exception, "Unhandled Exception!");
        }

        protected virtual void LoadWindow()
        {
            try
            {
                Window = GetRootWindow();
                var viewModel = GetRootDataContext();

                Window.Loaded += OnWindowLoaded;

                ViewModelBinder.BindView(viewModel, Window);
                App.MainWindow = Window;
                App.MainWindow.Show();
                App.MainWindow.Activate();
            }
            catch (Exception exception)
                when (exception is ComponentNotRegisteredException || exception is DependencyResolutionException)
            {
                Logger.Fatal(exception, "App crashed during Startup.");
                App.Shutdown(-1);
            }
        }

        protected virtual void OnBuilding()
        {
        }

        protected virtual void OnBuilt()
        {
            ConfigureServices();
            RegionManager = Container.Resolve<IRegionManager>();
        }

        protected virtual void OnExit(object sender, ExitEventArgs e)
        {
            Container.Dispose();
            Logger.Info("Bye 👋");
            LogManager.Flush();
            LogManager.Shutdown();
        }

        protected virtual void OnInitialized()
        {
        }

        protected virtual void OnInitializing()
        {
        }

        protected virtual void OnRegistered()
        {
        }

        protected virtual void OnRegistering()
        {
        }

        protected virtual void OnStartup(object sender, StartupEventArgs e)
        {
            Logger.Debug($"Application started by {sender} with the following arguments:\n{e.Args}");

            OnBuilding();
            Container = BuildIocContainer();
            OnBuilt();

            LoadWindow();
        }

        protected virtual void OnWindowLoaded(object sender, RoutedEventArgs e)
        {
            Window.Loaded -= OnWindowLoaded;

            var regionBuilder = Container.Resolve<IRegionBuilder>();

            RegisterRegions((DependencyObject)sender);
            BuildRegions(regionBuilder);
        }

        protected virtual void RegisterModules(ContainerBuilder builder)
        {
            builder.RegisterType<IRegionManager>().SingleInstance();
            // todo: Assembly Cache
            builder.RegisterAssemblyTypes(Assembly.GetExecutingAssembly()).AssignableTo<RegionBase>();
            builder.RegisterType<IRegionBuilder>().SingleInstance();
        }
    }
}