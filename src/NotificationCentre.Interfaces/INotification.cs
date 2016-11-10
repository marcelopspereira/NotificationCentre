using System;

namespace NotificationCentre.Interfaces
{
    public interface INotification
    {
        string Title { get; }

        string Content { get; }

        DateTime Timestamp { get; }
    }
}