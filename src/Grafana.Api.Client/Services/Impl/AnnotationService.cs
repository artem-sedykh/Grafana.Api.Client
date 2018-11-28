using System;
using System.Linq;
using System.Threading.Tasks;
using Grafana.Models;

namespace Grafana.Services.Impl
{
    public class AnnotationService: ServiceClient, IAnnotationService
    {
        private readonly IAuthenticationData _authentication;

        public AnnotationService(Uri baseUrl, string apiToken) : base(baseUrl)
        {
            _authentication = new BearerAuthentication(apiToken);
        }

        public Annotation[] FindAnnotations(long? from = null, long? to = null, int limit = 100, int? alertId = null,
            int? dashboardId = null, int? panelId = null, int? userId = null, AnnotationType? type = null,
            string[] tags = null)
        {
            var task = Task.Run(() => FindAnnotationsAsync(from, to, limit, alertId, dashboardId, panelId, userId, type, tags));

            task.Wait();

            return task.Result;
        }

        public async Task<Annotation[]> FindAnnotationsAsync(long? from = null, long? to = null, int limit = 100,
            int? alertId = null,
            int? dashboardId = null, int? panelId = null, int? userId = null, AnnotationType? type = null,
            string[] tags = null)
        {
            var parameters = new QueryStringParameters();

            if (from != null)
                parameters.Add(nameof(from), from);

            if (to != null)
                parameters.Add(nameof(to), to);

            parameters.Add(nameof(limit), limit);

            if (alertId != null)
                parameters.Add(nameof(alertId), alertId);

            if (dashboardId != null)
                parameters.Add(nameof(dashboardId), dashboardId);

            if (panelId != null)
                parameters.Add(nameof(panelId), panelId);

            if (userId != null)
                parameters.Add(nameof(userId), userId);

            if (type != null)
                parameters.Add(nameof(type), type.ToString().ToLowerInvariant());

            if (tags != null && tags.Any())
            {
                foreach (var tag in tags)
                    parameters.Add(nameof(tags), tag);
            }

            var response = await ExecuteGetRequestAsync<Annotation[]>("/api/annotations", parameters, _authentication);

            return response;
        }

        public CreateMessageResponse Create(CreateAnnotationRequest request)
        {
            if (request == null)
                throw new ArgumentNullException(nameof(request));

            var task = Task.Run(() => CreateAsync(request));

            task.Wait();

            return task.Result;
        }

        public async Task<CreateMessageResponse> CreateAsync(CreateAnnotationRequest request)
        {
            if (request == null)
                throw new ArgumentNullException(nameof(request));

            var response = await ExecutePostRequestAsync<CreateMessageResponse, CreateAnnotationRequest>("/api/annotations", null, request, _authentication);

            return response;
        }

        public CreateGraphiteMessageResponse Create(CreateGraphiteAnnotationRequest request)
        {
            if (request == null)
                throw new ArgumentNullException(nameof(request));

            var task = Task.Run(() => CreateAsync(request));

            task.Wait();

            return task.Result;
        }

        public async Task<CreateGraphiteMessageResponse> CreateAsync(CreateGraphiteAnnotationRequest request)
        {
            if (request == null)
                throw new ArgumentNullException(nameof(request));

            var response = await ExecutePostRequestAsync<CreateGraphiteMessageResponse, CreateGraphiteAnnotationRequest>("/api/annotations/graphite", null, request, _authentication);

            return response;
        }

        public MessageResponse Update(int annotationId, UpdateAnnotationRequest request)
        {
            if (request == null)
                throw new ArgumentNullException(nameof(request));

            var task = Task.Run(() => UpdateAsync(annotationId, request));

            task.Wait();

            return task.Result;
        }

        public async Task<MessageResponse> UpdateAsync(int annotationId, UpdateAnnotationRequest request)
        {
            if (request == null)
                throw new ArgumentNullException(nameof(request));

            var response = await ExecutePutRequestAsync<MessageResponse, UpdateAnnotationRequest>($"/api/annotations/{annotationId}", null, request, _authentication);

            return response;
        }

        public MessageResponse Delete(int annotationId)
        {
            var task = Task.Run(() => Delete(annotationId));

            task.Wait();

            return task.Result;
        }

        public async Task<MessageResponse> DeleteAsync(int annotationId)
        {
            var response = await ExecuteDeleteRequestAsync<MessageResponse, object>($"/api/annotations/{annotationId}", null, null, _authentication);

            return response;
        }

        public MessageResponse DeleteByRegionId(int regionId)
        {
            var task = Task.Run(() => DeleteByRegionIdAsync(regionId));

            task.Wait();

            return task.Result;
        }

        public async Task<MessageResponse> DeleteByRegionIdAsync(int regionId)
        {
            var response = await ExecuteDeleteRequestAsync<MessageResponse, object>($"/api/annotations/region/{regionId}", null, null, _authentication);

            return response;
        }
    }
}
