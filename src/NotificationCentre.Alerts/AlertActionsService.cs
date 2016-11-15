using System;
using System.ComponentModel.Composition;
using System.Reactive;
using System.Reactive.Linq;
using System.Reactive.Subjects;
using Presentation.Reactive.Concurrency;

namespace NotificationCentre.Alerts
{
    [Export(typeof(IAlertActionsService))]
    internal sealed class AlertActionsService : IAlertActionsService
    {
        private readonly ISubject<IAlertAction> _actions = new Subject<IAlertAction>();
        private readonly ISchedulerProvider _schedulerProvider;

        [ImportingConstructor]
        public AlertActionsService(ISchedulerProvider schedulerProvider)
        {
            _schedulerProvider = schedulerProvider;
        }

        public void OnAction(IAlert alert)
        {
            var action = new AlertAction(Actions.Action, alert);

            _actions.NotifyOn(_schedulerProvider.TaskPool)               
                    .OnNext(action);
        }

        public void OnDismiss(IAlert alert)
        {
            var action = new AlertAction(Actions.Dismiss, alert);

            _actions.NotifyOn(_schedulerProvider.TaskPool)
                    .OnNext(action);
        }

        public void OnTimeout(IAlert alert)
        {
            var action = new AlertAction(Actions.Timeout, alert);

            _actions.NotifyOn(_schedulerProvider.TaskPool)
                    .OnNext(action);
        }

        public IObservable<IAlertAction> ObserveActions()
        {
            return _actions.Publish()
                           .RefCount()
                           .AsObservable();
        }
    }
}