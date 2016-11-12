using System;

namespace NotificationCentre.Interfaces
{
    public interface INotificationManager
    {
        IObservable<INotification> ObserveNotifications();
    }
}