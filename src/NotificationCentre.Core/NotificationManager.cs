using System;
using NotificationCentre.Interfaces;
using System.ComponentModel.Composition;
using System.Reactive.Linq;

namespace NotificationCentre.Core
{
    [Export(typeof(INotificationManager))]
    internal sealed class NotificationManager : INotificationManager
    {
        public IObservable<INotification> ObserveNotifications()
        {
            return Observable.Empty<INotification>();
        }

        public void OnNext(INotification notification)
        {
            
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