using System;
using System.ComponentModel.Composition;
using System.Linq;
using System.Reactive;
using System.Reactive.Disposables;
using System.Reactive.Linq;
using System.Reactive.Subjects;
using NotificationCentre.Alerts.Models;
using NotificationCentre.Alerts.ViewModels;
using Presentation.Commands;
using Presentation.Core;
using Presentation.Reactive.Concurrency;

namespace NotificationCentre.Alerts.Controllers
{
    [PartCreationPolicy(CreationPolicy.NonShared)]
    internal sealed class AlertsViewModelController : IPartImportsSatisfiedNotification, IAlertsViewModelController, IDisposable
    {
        private readonly IAlertsQueue _alertsQueue;
        private readonly IAlertActionsService _alertActionsService;
        private readonly ISchedulerProvider _schedulerProvider;
        private readonly ISubject<Unit> _dequeueStream;
        private readonly CompositeDisposable _disposable = new CompositeDisposable();
        private readonly int _maximumAlerts = 4;

        [ImportingConstructor]
        public AlertsViewModelController(IAlertsQueue alertsQueue, ISchedulerProvider schedulerProvider, IAlertActionsService alertActionsService)
        {
            _alertsQueue = alertsQueue;
            _schedulerProvider = schedulerProvider;
            _alertActionsService = alertActionsService;
            _dequeueStream = new ReplaySubject<Unit>(1, _schedulerProvider.TaskPool);
            _disposable.Add((IDisposable)_dequeueStream);
        }

        [Export]
        public IAlertsViewModel ViewModel { get; } = new AlertsViewModel();

        public void OnImportsSatisfied()
        {
            var timeoutCommand = new DelegateCommand<IAlertModel>(a =>
            {
                var alert = a.ToAlert();
                _alertActionsService.OnTimeout(alert);
                a.Clear();
                _dequeueStream.OnNext(Unit.Default);
            });
            var dismissCommand = new DelegateCommand<IAlertModel>(a =>
            {
                var alert = a.ToAlert();
                _alertActionsService.OnDismiss(alert);
                a.Clear();
                _dequeueStream.OnNext(Unit.Default);
            });
            var actionCommand = new DelegateCommand<IAlertModel>(a =>
            {
                var alert = a.ToAlert();
                _alertActionsService.OnAction(alert);
                a.Clear();
                _dequeueStream.OnNext(Unit.Default);
            });

            for (int i = 0; i < _maximumAlerts; i++)
            {
                var alertModel = new AlertModel(timeoutCommand, dismissCommand, actionCommand);
                ViewModel.Alerts.Add(alertModel);
            }

            _dequeueStream.StartWith(Unit.Default, Unit.Default, Unit.Default, Unit.Default)
                          .Select(_ => _alertsQueue.Dequeue())
                          .SubscribeOn(_schedulerProvider.TaskPool)
                          .ObserveOn(_schedulerProvider.Dispatcher)
                          .Subscribe(alert =>
                          {
                              ViewModel.Alerts
                                       .FirstOrDefault(a => !a.HasAlert)
                                       ?.Update(alert);
                          })
                          .AddTo(_disposable);
        }

        public void Dispose()
        {
            _disposable.Dispose();
        }
    }
}