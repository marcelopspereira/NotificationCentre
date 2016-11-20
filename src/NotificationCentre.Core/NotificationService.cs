using System;
using System.ComponentModel.Composition;
using NotificationCentre.Interfaces;
using System.Reactive.Linq;

namespace NotificationCentre.Core
{
    [Export(typeof(INotificationService))]
    internal sealed class NotificationService : INotificationService
    {
        private readonly INotificationManager _notificationManager;

        [ImportingConstructor]
        public NotificationService(INotificationManager notificationManager)
        {
            _notificationManager = notificationManager;
        }

        public void Post(INotification notification)
        {
            if (notification == null)
                throw new ArgumentNullException(nameof(notification));
            
            _notificationManager.OnNext(notification);
        }

        public IObservable<INotification> ObserveDismissed()
        {
            return Observable.Empty<INotification>();
        }

        public IObservable<INotification> ObserveTimedOut()
        {
            return Observable.Empty<INotification>();
        }

        public IObservable<INotification> ObserveActivated()
        {
            return Observable.Empty<INotification>();
        }
    }
}