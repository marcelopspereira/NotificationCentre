using System;
using NotificationCentre.Interfaces;
using System.ComponentModel.Composition;
using System.Reactive.Linq;
using System.Reactive.Subjects;

namespace NotificationCentre.Core
{
    [Export(typeof(INotificationManager))]
    internal sealed class NotificationManager : INotificationManager
    {
        private readonly ISubject<INotification> _notifications = new Subject<INotification>();

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
            
        }

        public void OnTimeout(string id)
        {
            
        }

        public void OnDismiss(string id)
        {
            
        }
    }
}