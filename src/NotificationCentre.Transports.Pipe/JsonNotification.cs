using System;
using NotificationCentre.Interfaces;

namespace NotificationCentre.Transports
{
    internal sealed class JsonNotification : INotification
    {
        public JsonNotification(string id, string title, string content, DateTime timestamp)
        {
            Id = id;
            Title = title;
            Content = content;
            Timestamp = timestamp;
        }

        public string Id { get; }

        public string Title { get; }

        public string Content { get; }

        public DateTime Timestamp { get; }
    }
}