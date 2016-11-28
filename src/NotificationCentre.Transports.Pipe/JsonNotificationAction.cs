using Newtonsoft.Json;
using NotificationCentre.Interfaces;
using System;

namespace NotificationCentre.Transports
{
    internal sealed class JsonNotificationAction : INotificationAction
    {
        public JsonNotificationAction(string id, DateTime timestamp)
        {
            Id = id;
            Timestamp = timestamp;
        }

        [JsonProperty("id")]
        public string Id { get; }

        [JsonProperty("timestamp")]
        public DateTime Timestamp { get; }
    }
}
