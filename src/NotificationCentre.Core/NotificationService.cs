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

        public IObservable<INotification> ObserveDismissed()
        {
            throw new NotImplementedException();
        }

        public IObservable<INotification> ObserveTimedOut()
        {
            throw new NotImplementedException();
        }

        public IObservable<INotification> ObserveActivated()
        {
            throw new NotImplementedException();
        }
    }
}