using System;

namespace NotificationCentre.Alerts.Models
{
    internal static class AlertModelExtensions
    {
        public static void Clear(this IAlertModel alertModel)
        {
            if (alertModel == null)
                throw new ArgumentNullException(nameof(alertModel));

            alertModel.HasAlert = false;
        }

        public static void Update(this IAlertModel alertModel, IAlert alert)
        {
            if (alertModel == null)
                throw new ArgumentNullException(nameof(alertModel));

            alertModel.Id = alert.Id;
            alertModel.Title = alert.Title;
            alertModel.Content = alert.Content;
            alertModel.HasAlert = true;
        }

        public static IAlert ToAlert(this IAlertModel alertModel)
        {
            return new Alert(alertModel.Id, alertModel.Title, alertModel.Content);
        }
    }
}