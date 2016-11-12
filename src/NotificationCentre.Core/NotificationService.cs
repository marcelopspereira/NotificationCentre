using System;
using System.ComponentModel.Composition;
using NotificationCentre.Interfaces;

namespace NotificationCentre.Core
{
    [Export(typeof(INotificationService))]
    internal sealed class NotificationService : INotificationService
    {
        public void Post(INotification notification)
        {
            if (notification == null)
                throw new ArgumentNullException(nameof(notification));
            
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