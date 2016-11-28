using System;

namespace NotificationCentre.Interfaces
{
    public interface INotificationAction
    {
        string Id { get; }

        DateTime Timestamp { get; }
    }
}
