using System;
using NotificationCentre.Interfaces;
using System.ComponentModel.Composition;
using System.Reactive.Linq;
using System.Reactive.Subjects;
using System.Reactive.Disposables;

namespace NotificationCentre.Core
{
    [Export(typeof(INotificationManager))]
    internal sealed class NotificationManager : INotificationManager, IDisposable
    {
        private readonly ISubject<INotificationAction> _activated = new Subject<INotificationAction>();
        private readonly ISubject<INotificationAction> _dismissed = new Subject<INotificationAction>();
        private readonly ISubject<INotificationAction> _timedout = new Subject<INotificationAction>();
        private readonly ISubject<INotification> _notifications = new Subject<INotification>();
        private readonly CompositeDisposable _disposable;

        public NotificationManager()
        {
            _disposable = new CompositeDisposable((IDisposable)_activated, (IDisposable)_dismissed, (IDisposable)_timedout, (IDisposable)_notifications);
        }

        public IObservable<INotification> ObserveNotifications()
        {
            return _notifications.AsObservable();
        }

        public void OnNext(INotification notification)
        {
            _notifications.OnNext(notification);
        }

        public void OnAction(string id)
        {
            var notificationAction = new NotificationAction(id, DateTime.UtcNow);

            _activated.OnNext(notificationAction);
        }

        public void OnTimeout(string id)
        {
            var notificationAction = new NotificationAction(id, DateTime.UtcNow);

            _timedout.OnNext(notificationAction);
        }

        public void OnDismiss(string id)
        {
            var notificationAction = new NotificationAction(id, DateTime.UtcNow);

            _dismissed.OnNext(notificationAction);
        }

        public IObservable<INotificationAction> ObservedActivated()
        {
            return _activated.AsObservable();
        }

        public IObservable<INotificationAction> ObservedDismissed()
        {
            return _dismissed.AsObservable();
        }

        public IObservable<INotificationAction> ObservedTimedout()
        {
            return _timedout.AsObservable();
        }

        public void Dispose()
        {
            _disposable.Dispose();
        }
    }
}