using System;

namespace NotificationCentre.Interfaces
{
    public interface INotification
    {
        string Id { get; }

        string Title { get; }

        string Content { get; }

        DateTime Timestamp { get; }
    }
}