using System;

namespace NotificationCentre.Interfaces
{
    public interface INotificationManager
    {
        IObservable<INotification> ObserveNotifications();

        IObservable<INotificationAction> ObservedActivated();

        IObservable<INotificationAction> ObservedDismissed();

        IObservable<INotificationAction> ObservedTimedout();

        void OnNext(INotification notification);

        void OnAction(string id);

        void OnTimeout(string id);

        void OnDismiss(string id);
    }
}