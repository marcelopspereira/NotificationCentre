using System;
using NotificationCentre.Interfaces;

namespace NotificationCentre.Transports
{
    internal sealed class JsonNotification : INotification
    {
        public JsonNotification(string title, string content, DateTime timestamp)
        {
            Title = title;
            Content = content;
            Timestamp = timestamp;
        }

        public string Title { get; }

        public string Content { get; }

        public DateTime Timestamp { get; }
    }
}