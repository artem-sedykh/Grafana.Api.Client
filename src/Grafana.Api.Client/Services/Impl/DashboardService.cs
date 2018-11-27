using System;
using Grafana.Models;

namespace Grafana.Services.Impl
{
    public class DashboardService : ServiceClient, IDashboardService
    {
        private readonly IAuthenticationData _authentication;

        public DashboardService(Uri baseUrl, string apiToken) : base(baseUrl)
        {
            _authentication = new BearerAuthentication(apiToken);
        }

        public CreateDashboardResponse Create(CreateDashboardRequest request)
        {
            if (request == null)
                throw new ArgumentNullException(nameof(request));

            var response = ExecutePostRequest<CreateDashboardResponse, CreateDashboardRequest>("/api/dashboards/db", null, request, _authentication);

            return response;
        }

        public UpdateDashboardResponse Update(UpdateDashboardRequest request)
        {
            if (request == null)
                throw new ArgumentNullException(nameof(request));

            var response =
                ExecutePostRequest<UpdateDashboardResponse, UpdateDashboardRequest>("/api/dashboards/db", null, request,
                    _authentication);

            return response;
        }

        public DashboardResponse GetDashboardByUid(string uId)
        {
            if (string.IsNullOrWhiteSpace(uId))
                throw new ArgumentNullException(nameof(uId));

            var response = ExecuteGetRequest<DashboardResponse>($"/api/dashboards/uid/{uId}", null, _authentication);

            return response;
        }

        public DashboardResponse GetDashboardBySlug(string slug)
        {
            if (string.IsNullOrWhiteSpace(slug))
                throw new ArgumentNullException(nameof(slug));

            var response = ExecuteGetRequest<DashboardResponse>($"/api/dashboards/db/{slug}", null, _authentication);

            return response;
        }

        public void DeleteDashboardByUid(string uId)
        {
            if (string.IsNullOrWhiteSpace(uId))
                throw new ArgumentNullException(nameof(uId));

            ExecuteDeleteRequest<object, object>($"/api/dashboards/uid/{uId}", null, null, _authentication);
        }

        public void DeleteDashboardBySlug(string slug)
        {
            if (string.IsNullOrWhiteSpace(slug))
                throw new ArgumentNullException(nameof(slug));

            ExecuteDeleteRequest<object, object>($"/api/dashboards/db/{slug}", null, null, _authentication);
        }
    }
}
