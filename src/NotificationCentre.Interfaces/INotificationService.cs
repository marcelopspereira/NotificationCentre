using System;

namespace NotificationCentre.Interfaces
{
    public interface INotificationService
    {
        void Post(INotification notification);

        IObservable<INotificationAction> ObserveDismissed();

        IObservable<INotificationAction> ObserveTimedOut();

        IObservable<INotificationAction> ObserveActivated();
    }
}