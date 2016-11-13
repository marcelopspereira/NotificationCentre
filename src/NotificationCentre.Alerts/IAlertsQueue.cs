namespace NotificationCentre.Alerts
{
    internal interface IAlertsQueue
    {
        void Enqueue(IAlert alert);

        IAlert Dequeue();
    }
}