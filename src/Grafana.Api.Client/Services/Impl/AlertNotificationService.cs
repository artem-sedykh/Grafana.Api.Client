using System;
using Grafana.Models;

namespace Grafana.Services.Impl
{
    public class AlertNotificationService : ServiceClient, IAlertNotificationService
    {
        private readonly IAuthenticationData _authentication;

        public AlertNotificationService(Uri baseUrl, string apiToken) : base(baseUrl)
        {
            _authentication = new BearerAuthentication(apiToken);
        }

        public AlertNotification[] GetAlertNotifications()
        {
            var response = ExecuteGetRequest<AlertNotification[]>("/api/alert-notifications", null, _authentication);

            return response;
        }

        public CreateAlertNotificationResponse CreateAlertNotification(CreateAlertNotificationRequest request)
        {
            if (request == null)
                throw new ArgumentNullException(nameof(request));

            var response = ExecutePostRequest<CreateAlertNotificationResponse, CreateAlertNotificationRequest>("/api/alert-notifications", null, request, _authentication);

            return response;
        }

        public UpdateAlertNotificationResponse UpdateAlertNotification(UpdateAlertNotificationRequest request)
        {
            if (request == null)
                throw new ArgumentNullException(nameof(request));

            var response = ExecutePutRequest<UpdateAlertNotificationResponse, UpdateAlertNotificationRequest>($"/api/alert-notifications/{request.Id}", null, request, _authentication);

            return response;
        }

        public MessageResponse DeleteAlertNotification(int notificationId)
        {
            var response = ExecuteDeleteRequest<MessageResponse, object>($"/api/alert-notifications/{notificationId}", null, null, _authentication);

            return response;
        }
    }
}
