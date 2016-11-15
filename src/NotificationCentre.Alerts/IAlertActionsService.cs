using System;

namespace NotificationCentre.Alerts
{
    internal interface IAlertActionsService
    {
        void OnAction(IAlert alert);

        void OnDismiss(IAlert alert);

        void OnTimeout(IAlert alert);

        IObservable<IAlertAction> ObserveActions();
    }
}