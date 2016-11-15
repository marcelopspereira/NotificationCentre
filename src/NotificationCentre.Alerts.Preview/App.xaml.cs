using System;
using System.Reactive.Linq;
using System.Windows;
using Moq;
using NotificationCentre.Alerts.Controllers;
using Presentation.Reactive.Concurrency;

namespace NotificationCentre.Alerts.Preview
{
    public partial class App
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            var alertsQueue = new BlockingAlertsQueue();
            var schedulerProvider = new DefaultSchedulerProvider();
            var alertActions = new AlertActionsService(schedulerProvider);

            var viewController = new AlertsViewController();
            viewController.OnImportsSatisfied();
            var viewModelController = new AlertsViewModelController(alertsQueue, schedulerProvider, alertActions);
            viewModelController.OnImportsSatisfied();

            var view = viewController.View;
            view.DataContext = viewModelController.ViewModel;
            view.Show();

            var alertModel = new Mock<IAlert>();
            alertModel.Setup(a => a.Title).Returns("Update Available");
            alertModel.Setup(a => a.Content).Returns("Restart to use the new version.");

            Observable.Interval(TimeSpan.FromSeconds(15))
                      .Select(_ => alertModel.Object)
                      .Subscribe(alert => alertsQueue.Enqueue(alert));
        }
    }
}
