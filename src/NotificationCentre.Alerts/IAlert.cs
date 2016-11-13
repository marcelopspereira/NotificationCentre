namespace NotificationCentre.Alerts
{
    internal interface IAlert
    {
        string Id { get; }

        string Content { get; }

        string Title { get; }
    }
}