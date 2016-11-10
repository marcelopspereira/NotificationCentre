using NotificationCentre.Interfaces;

namespace NotificationCentre.SideBar.Models
{
    internal static class NotificationModelExtensions
    {
        public static INotificationModel ToModel(this INotification notification)
        {
            return new NotificationModel(notification.Title, notification.Content, notification.Timestamp);
        }
    }
}