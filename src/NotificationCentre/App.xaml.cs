using System;
using System.ComponentModel.Composition.Hosting;
using System.Windows;
using Presentation.Bootstrappers;
using Presentation.Interfaces;

namespace NotificationCentre
{    
    public partial class App
    {
        private readonly IBootstrapper _bootstrapper;

        public App()
        {
            var catalog = new DirectoryCatalog(Environment.CurrentDirectory);

            var container = new CompositionContainer(catalog);

            var bootstrapper = new DelegateBootstrapper(() => {}, container.Dispose);

            _bootstrapper = bootstrapper.InitalizeModules(container)
                                        .CatchDispatcherExceptions(this, ex => true)
                                        .CatchTaskExceptions(ex => true)
                                        .CatchAppDomainExceptions(AppDomain.CurrentDomain, (ex, handled) => {});
        }

        protected override void OnStartup(StartupEventArgs e)
        {
            _bootstrapper.Initialize();
        }

        protected override void OnExit(ExitEventArgs e)
        {
            _bootstrapper.Dispose();
        }
    }
}
