using System;
using System.Reactive.Linq;
using System.Threading;
using System.Windows;
using NotificationCentre.Alerts.Controllers;
using NotificationCentre.Alerts.Models;

namespace NotificationCentre.Alerts.Preview
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            var viewController = new AlertsViewController();
            viewController.OnImportsSatisfied();
            var viewModelController = new AlertsViewModelController(null);
            viewModelController.OnImportsSatisfied();

            var view = viewController.View;
            view.DataContext = viewModelController.ViewModel;
            view.Show();

            Observable.Interval(TimeSpan.FromSeconds(5))
                      .ObserveOn(SynchronizationContext.Current)
                      .Select(_ => new AlertModel { Title = "Update Available", Content = "Restart to use the new version.", HasAlert = true})
                      .Subscribe(alert => viewModelController.ViewModel.Alerts.Add(alert));
        }
    }
}
