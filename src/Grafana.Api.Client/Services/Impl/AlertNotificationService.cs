using System;
using System.Threading.Tasks;
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
            var task = Task.Run(GetAlertNotificationsAsync);

            task.Wait();

            return task.Result;
        }

        public async Task<AlertNotification[]> GetAlertNotificationsAsync()
        {
            var response = await ExecuteGetRequestAsync<AlertNotification[]>("/api/alert-notifications", null, _authentication);

            return response;
        }

        public CreateAlertNotificationResponse CreateAlertNotification(CreateAlertNotificationRequest request)
        {
            if (request == null)
                throw new ArgumentNullException(nameof(request));

            var task = Task.Run(() => CreateAlertNotificationAsync(request));

            task.Wait();

            return task.Result;
        }

        public async Task<CreateAlertNotificationResponse> CreateAlertNotificationAsync(CreateAlertNotificationRequest request)
        {
            if (request == null)
                throw new ArgumentNullException(nameof(request));

            var response = await ExecutePostRequestAsync<CreateAlertNotificationResponse, CreateAlertNotificationRequest>("/api/alert-notifications", null, request, _authentication);

            return response;
        }

        public UpdateAlertNotificationResponse UpdateAlertNotification(UpdateAlertNotificationRequest request)
        {
            if (request == null)
                throw new ArgumentNullException(nameof(request));

            var task = Task.Run(() => UpdateAlertNotificationAsync(request));

            task.Wait();

            return task.Result;
        }

        public async Task<UpdateAlertNotificationResponse> UpdateAlertNotificationAsync(UpdateAlertNotificationRequest request)
        {
            if (request == null)
                throw new ArgumentNullException(nameof(request));

            var response = await ExecutePutRequestAsync<UpdateAlertNotificationResponse, UpdateAlertNotificationRequest>($"/api/alert-notifications/{request.Id}", null, request, _authentication);

            return response;
        }

        public MessageResponse DeleteAlertNotification(int notificationId)
        {
            var task = Task.Run(() => DeleteAlertNotificationAsync(notificationId));

            task.Wait();

            return task.Result;
        }

        public async Task<MessageResponse> DeleteAlertNotificationAsync(int notificationId)
        {
            var response = await ExecuteDeleteRequestAsync<MessageResponse, object>($"/api/alert-notifications/{notificationId}", null, null, _authentication);

            return response;
        }
    }
}
