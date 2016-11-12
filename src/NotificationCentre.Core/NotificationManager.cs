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
    }
}