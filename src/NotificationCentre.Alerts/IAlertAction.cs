namespace NotificationCentre.Alerts
{
    internal interface IAlertAction
    {
        Actions Action { get; }

        IAlert Alert { get; }
    }
}