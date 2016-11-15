using System;

namespace NotificationCentre.Interfaces
{
    public interface INotificationManager
    {
        IObservable<INotification> ObserveNotifications();

        void OnNext(INotification notification);

        void OnAction(string id);

        void OnTimeout(string id);

        void OnDismiss(string id);
    }
}