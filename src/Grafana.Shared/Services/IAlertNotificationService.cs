using Grafana.Models;

namespace Grafana.Services
{
    public interface IAlertNotificationService
    {
        /// <summary>
        /// <see cref="http://docs.grafana.org/http_api/alerting/"/>
        /// </summary>
        AlertNotification[] GetAlertNotifications();

        /// <summary>
        /// <see cref="http://docs.grafana.org/http_api/alerting/"/>
        /// </summary>
        CreateAlertNotificationResponse CreateAlertNotification(CreateAlertNotificationRequest request);

        /// <summary>
        /// <see cref="http://docs.grafana.org/http_api/alerting/"/>
        /// </summary>
        UpdateAlertNotificationResponse UpdateAlertNotification(UpdateAlertNotificationRequest request);

        /// <summary>
        /// <see cref="http://docs.grafana.org/http_api/alerting/"/>
        /// </summary>
        MessageResponse DeleteAlertNotification(int notificationId);


    }
}
