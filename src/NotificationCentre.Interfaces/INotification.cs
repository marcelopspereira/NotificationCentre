using System;

namespace NotificationCentre.Interfaces
{
    internal interface INotification
    {
        string Title { get; }

        string Content { get; }

        DateTime Timestamp { get; }
    }
}