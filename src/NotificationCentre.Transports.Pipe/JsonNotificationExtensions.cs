using NotificationCentre.Interfaces;

namespace NotificationCentre.Transports
{
    internal static class JsonNotificationExtensions
    {
        public static JsonNotification ToJsonNotification(this INotification notification)
        {
            return new JsonNotification(notification.Id, notification.Title, notification.Content, notification.Timestamp);
        }
    }
}
