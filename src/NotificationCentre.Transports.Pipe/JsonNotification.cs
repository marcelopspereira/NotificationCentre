using System;
using NotificationCentre.Interfaces;
using Newtonsoft.Json;

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

        [JsonProperty("id")]
        public string Id { get; }

        [JsonProperty("title")]
        public string Title { get; }

        [JsonProperty("content")]
        public string Content { get; }

        [JsonProperty("timestamp")]
        public DateTime Timestamp { get; }
    }
}