using System;
using System.Reactive.Linq;
using System.Threading;
using System.Windows;
using Moq;
using NotificationCentre.Alerts.Controllers;
using NotificationCentre.Alerts.Models;
using NotificationCentre.Core;

namespace NotificationCentre.Alerts.Preview
{
    public partial class App
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            var notificationManger = new NotificationManager();

            var viewController = new AlertsViewController();
            viewController.OnImportsSatisfied();
            var viewModelController = new AlertsViewModelController(notificationManger);
            viewModelController.OnImportsSatisfied();

            var view = viewController.View;
            view.DataContext = viewModelController.ViewModel;
            view.Show();

            var alertModel = new Mock<IAlertModel>();
            alertModel.Setup(a => a.Title).Returns("Update Available");
            alertModel.Setup(a => a.Content).Returns("Restart to use the new version.");
            alertModel.Setup(a => a.HasAlert).Returns(true);

            Observable.Interval(TimeSpan.FromSeconds(5))
                      .ObserveOn(SynchronizationContext.Current)
                      .Select(_ => alertModel.Object)
                      .Subscribe(alert => viewModelController.ViewModel.Alerts.Add(alert));
        }
    }
}
