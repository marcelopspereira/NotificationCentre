using NotificationCentre.Interfaces;
using System;

namespace NotificationCentre.Core
{
    internal sealed class NotificationAction : INotificationAction
    {
        public NotificationAction(string id, DateTime timestamp)
        {
            Id = id;
            Timestamp = timestamp;
        }

        public string Id { get; }

        public DateTime Timestamp { get; }
    }
}
