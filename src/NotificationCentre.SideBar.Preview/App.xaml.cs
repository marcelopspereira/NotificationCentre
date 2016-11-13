using System;
using System.Reactive.Linq;
using System.Threading;
using System.Windows;
using NotificationCentre.Core;
using NotificationCentre.SideBar.Controllers;
using NotificationCentre.SideBar.Models;
using Presentation.Interop;
using Presentation.Reactive.Concurrency;

namespace NotificationCentre.SideBar.Preview
{
    public partial class App
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            var hideFromAppSwitch = new HideFromAppSwitchService();
            var hideFromPeekService = new HideFromPeekService();
            var notificationManager = new NotificationManager();
            var schedulerProvider = new DefaultSchedulerProvider();

            var viewController = new SideBarViewController(hideFromAppSwitch, hideFromPeekService);
            viewController.OnImportsSatisfied();
            var viewModelController = new SideBarViewModelController(notificationManager, schedulerProvider);
            viewModelController.OnImportsSatisfied();

            var view = viewController.View;
            view.DataContext = viewModelController.ViewModel;
            view.Show();

            Observable.Timer(TimeSpan.FromSeconds(5))
                      .ObserveOn(SynchronizationContext.Current)
                      .Subscribe(_ => viewModelController.ViewModel.IsOpen = true);
            Observable.Interval(TimeSpan.FromSeconds(5))
                      .ObserveOn(SynchronizationContext.Current)
                      .Select(_ => new NotificationModel("Update Available", "Restart to use the new version.", DateTime.Now))
                      .Subscribe(alert => viewModelController.ViewModel.Notifications.Insert(0, alert));
            Observable.Timer(TimeSpan.FromSeconds(60))
                      .ObserveOn(SynchronizationContext.Current)
                      .Subscribe(_ => viewModelController.ViewModel.IsOpen = false);
        }
    }
}
