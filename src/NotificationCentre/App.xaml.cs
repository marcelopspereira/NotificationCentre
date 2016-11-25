using System;
using System.ComponentModel.Composition.Hosting;
using System.Windows;
using Presentation.Bootstrappers;
using Presentation.Interfaces;
using NLog;

namespace NotificationCentre
{    
    public partial class App
    {
        private readonly ILogger _logger = LogManager.GetCurrentClassLogger();
        private readonly IBootstrapper _bootstrapper;

        public App()
        {
            var catalog = new DirectoryCatalog(Environment.CurrentDirectory);

            var container = new CompositionContainer(catalog);

            var bootstrapper = new DelegateBootstrapper(() => {}, container.Dispose);

            _bootstrapper = bootstrapper.InitalizeModules(container)
                                        .CatchDispatcherExceptions(this, ex =>
                                        {
                                            _logger.Error(ex);
                                            return true;
                                        })
                                        .CatchTaskExceptions(ex => 
                                        {
                                            _logger.Error(ex);
                                            return true;
                                        })
                                        .CatchAppDomainExceptions(AppDomain.CurrentDomain, (ex, handled) => 
                                        {
                                            _logger.Error(ex);
                                        });
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
