using System;
using System.ComponentModel.Composition;
using NotificationCentre.Interfaces;

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

        public IObservable<INotificationAction> ObserveDismissed()
        {
            return _notificationManager.ObservedDismissed();
        }

        public IObservable<INotificationAction> ObserveTimedOut()
        {
            return _notificationManager.ObservedTimedout();
        }

        public IObservable<INotificationAction> ObserveActivated()
        {
            return _notificationManager.ObservedActivated();
        }
    }
}