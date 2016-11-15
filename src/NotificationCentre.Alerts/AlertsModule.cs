using System;
using System.ComponentModel.Composition;
using System.Reactive.Disposables;
using System.Reactive.Linq;
using NotificationCentre.Interfaces;
using Presentation.Core;
using Presentation.Interfaces;
using Presentation.Reactive.Concurrency;

namespace NotificationCentre.Alerts
{
    [Export(typeof(IModule))]
    internal sealed class AlertsModule : IModule
    {
        private readonly INotificationManager _notificationManager;
        private readonly IAlertsQueue _alertsQueue;
        private readonly IAlertActionsService _alertActionsService;
        private readonly ISchedulerProvider _schedulerProvider;
        private readonly CompositeDisposable _disposable = new CompositeDisposable();

        [ImportingConstructor]
        public AlertsModule(INotificationManager notificationManager, 
                            IAlertsQueue alertsQueue, 
                            ISchedulerProvider schedulerProvider, 
                            IAlertActionsService alertActionsService)
        {
            _notificationManager = notificationManager;
            _alertsQueue = alertsQueue;
            _schedulerProvider = schedulerProvider;
            _alertActionsService = alertActionsService;
        }

        public void Dispose()
        {
            _disposable.Dispose();
        }

        public void Initialize()
        {
            var actions = _alertActionsService.ObserveActions();

            actions.Where(a => a.Action == Actions.Action)
                   .Select(a => a.Alert)
                   .SubscribeOn(_schedulerProvider.TaskPool)
                   .ObserveOn(_schedulerProvider.TaskPool)
                   .Subscribe(alert => _notificationManager.OnAction(alert.Id))
                   .AddTo(_disposable);

            actions.Where(a => a.Action == Actions.Dismiss)
                   .Select(a => a.Alert)
                   .SubscribeOn(_schedulerProvider.TaskPool)
                   .ObserveOn(_schedulerProvider.TaskPool)
                   .Subscribe(alert => _notificationManager.OnDismiss(alert.Id))
                   .AddTo(_disposable);

            actions.Where(a => a.Action == Actions.Timeout)
                   .Select(a => a.Alert)
                   .SubscribeOn(_schedulerProvider.TaskPool)
                   .ObserveOn(_schedulerProvider.TaskPool)
                   .Subscribe(alert => _notificationManager.OnTimeout(alert.Id))
                   .AddTo(_disposable);

            _notificationManager.ObserveNotifications()
                                .Select(notification => notification.ToAlert())
                                .SubscribeOn(_schedulerProvider.TaskPool)
                                .ObserveOn(_schedulerProvider.TaskPool)
                                .Subscribe(alert => _alertsQueue.Enqueue(alert))
                                .AddTo(_disposable);
        }
    }
}