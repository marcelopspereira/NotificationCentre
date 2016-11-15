namespace NotificationCentre.Alerts
{
    internal sealed class AlertAction : IAlertAction
    {
        public AlertAction(Actions action, IAlert alert)
        {
            Action = action;
            Alert = alert;
        }

        public Actions Action { get; }

        public IAlert Alert { get; }
    }
}