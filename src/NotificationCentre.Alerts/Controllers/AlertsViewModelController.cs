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
        private readonly ISchedulerProvider _schedulerProvider;
        private readonly ISubject<Unit> _dequeueStream = new Subject<Unit>();
        private readonly CompositeDisposable _disposable = new CompositeDisposable();
        private readonly int _maximumAlerts = 4;

        [ImportingConstructor]
        public AlertsViewModelController(IAlertsQueue alertsQueue, ISchedulerProvider schedulerProvider)
        {
            _alertsQueue = alertsQueue;
            _schedulerProvider = schedulerProvider;
            _disposable.Add((IDisposable)_dequeueStream);
        }

        [Export]
        public IAlertsViewModel ViewModel { get; } = new AlertsViewModel();

        public void OnImportsSatisfied()
        {
            var timeoutCommand = new DelegateCommand<IAlertModel>(a =>
            {
                var alert = a.ToAlert();
                a.Clear();
                _dequeueStream.OnNext(Unit.Default);
            });
            var dismissCommand = new DelegateCommand<IAlertModel>(a =>
            {
                var alert = a.ToAlert();
                a.Clear();
                _dequeueStream.OnNext(Unit.Default);
            });
            var actionCommand = new DelegateCommand<IAlertModel>(a =>
            {
                var alert = a.ToAlert();
                a.Clear();
                _dequeueStream.OnNext(Unit.Default);
            });

            for (int i = 0; i < _maximumAlerts; i++)
            {
                var alertModel = new AlertModel(timeoutCommand, dismissCommand, actionCommand);
                ViewModel.Alerts.Add(alertModel);
            }

            _dequeueStream.StartWith(Unit.Default)
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