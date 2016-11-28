using NotificationCentre.Interfaces;

namespace NotificationCentre.Transports
{
    internal static class JsonNotificationActionExtensions
    {
        public static JsonNotificationAction ToJsonNotification(this INotificationAction notificationAction)
        {
            return new JsonNotificationAction(notificationAction.Id, notificationAction.Timestamp);
        }
    }
}
