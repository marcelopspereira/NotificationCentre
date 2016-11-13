using System;
using System.Windows.Input;
using NotificationCentre.Interfaces;

namespace NotificationCentre.Alerts.Models
{
    internal static class AlertModelExtensions
    {
        public static IAlertModel ToModel(this INotification notification, ICommand timeout, ICommand dismiss, ICommand action)
        {
            if (notification == null)
                throw new ArgumentNullException(nameof(notification));
            
            return new AlertModel(notification.Id, notification.Title, notification.Content, true, timeout, dismiss, action);
        }

        public static void Clear(this IAlertModel alertModel)
        {
            if (alertModel == null)
                throw new ArgumentNullException(nameof(alertModel));

            alertModel.HasAlert = false;
        }
    }
}