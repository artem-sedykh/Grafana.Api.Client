using System;
using System.Threading.Tasks;
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

            var task = Task.Run(() => CreateAsync(request));

            task.Wait();

            return task.Result;
        }

        public async Task<CreateDashboardResponse> CreateAsync(CreateDashboardRequest request)
        {
            if (request == null)
                throw new ArgumentNullException(nameof(request));

            var response = await ExecutePostRequestAsync<CreateDashboardResponse, CreateDashboardRequest>("/api/dashboards/db", null, request, _authentication);

            return response;
        }

        public UpdateDashboardResponse Update(UpdateDashboardRequest request)
        {
            if (request == null)
                throw new ArgumentNullException(nameof(request));

            var task = Task.Run(() => UpdateAsync(request));

            task.Wait();

            return task.Result;
        }

        public async Task<UpdateDashboardResponse> UpdateAsync(UpdateDashboardRequest request)
        {
            if (request == null)
                throw new ArgumentNullException(nameof(request));

            var response =await ExecutePostRequestAsync<UpdateDashboardResponse, UpdateDashboardRequest>("/api/dashboards/db", null, request, _authentication);

            return response;
        }

        public DashboardResponse GetDashboardByUid(string uId)
        {
            if (string.IsNullOrWhiteSpace(uId))
                throw new ArgumentNullException(nameof(uId));

            var task = Task.Run(() => GetDashboardByUidAsync(uId));

            task.Wait();

            return task.Result;
        }

        public async Task<DashboardResponse> GetDashboardByUidAsync(string uId)
        {
            if (string.IsNullOrWhiteSpace(uId))
                throw new ArgumentNullException(nameof(uId));

            var response = await ExecuteGetRequestAsync<DashboardResponse>($"/api/dashboards/uid/{uId}", null, _authentication);

            return response;
        }

        public DashboardResponse GetDashboardBySlug(string slug)
        {
            if (string.IsNullOrWhiteSpace(slug))
                throw new ArgumentNullException(nameof(slug));

            var task = Task.Run(() => GetDashboardBySlugAsync(slug));

            task.Wait();

            return task.Result;
        }

        public async Task<DashboardResponse> GetDashboardBySlugAsync(string slug)
        {
            if (string.IsNullOrWhiteSpace(slug))
                throw new ArgumentNullException(nameof(slug));

            var response = await ExecuteGetRequestAsync<DashboardResponse>($"/api/dashboards/db/{slug}", null, _authentication);

            return response;
        }

        public void DeleteDashboardByUid(string uId)
        {
            if (string.IsNullOrWhiteSpace(uId))
                throw new ArgumentNullException(nameof(uId));

            var task = Task.Run(() => DeleteDashboardByUidAsync(uId));

            task.Wait();
        }

        public async Task DeleteDashboardByUidAsync(string uId)
        {
            if (string.IsNullOrWhiteSpace(uId))
                throw new ArgumentNullException(nameof(uId));

            await ExecuteDeleteRequestAsync<object, object>($"/api/dashboards/uid/{uId}", null, null, _authentication);
        }

        public void DeleteDashboardBySlug(string slug)
        {
            if (string.IsNullOrWhiteSpace(slug))
                throw new ArgumentNullException(nameof(slug));

            var task = Task.Run(() => DeleteDashboardBySlugAsync(slug));

            task.Wait();
        }

        public async Task DeleteDashboardBySlugAsync(string slug)
        {
            if (string.IsNullOrWhiteSpace(slug))
                throw new ArgumentNullException(nameof(slug));

            await ExecuteDeleteRequestAsync<object, object>($"/api/dashboards/db/{slug}", null, null, _authentication);
        }
    }
}
