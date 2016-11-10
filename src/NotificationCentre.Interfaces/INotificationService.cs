using System;

namespace NotificationCentre.Interfaces
{
    public interface INotificationService
    {
        void Post(INotification notification);

        IObservable<INotification> ObserveNotifications();
    }
}