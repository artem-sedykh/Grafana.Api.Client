﻿using System;
using System.Threading.Tasks;
using Grafana.Models;

namespace Grafana.Services.Impl
{
    public class AlertService: ServiceClient,IAlertService
    {
        private readonly IAuthenticationData _authentication;

        public AlertService(Uri baseUrl, string apiToken) : base(baseUrl)
        {
            _authentication = new BearerAuthentication(apiToken);
        }

        public Alert[] GetAlerts(int[] dashboardIds = null, int? panelId = null, string query = null, AlertState[] states = null,
            int? limit = null, int[] folderIds = null, string dashboardQuery = null, string[] dashboardTags = null)
        {
            var task = Task.Run(() => GetAlertsAsync(dashboardIds, panelId, query, states, limit, folderIds, dashboardQuery, dashboardTags));

            task.Wait();

            return task.Result;
        }

        public async Task<Alert[]> GetAlertsAsync(int[] dashboardIds = null, int? panelId = null, string query = null, AlertState[] states = null,
            int? limit = null, int[] folderIds = null, string dashboardQuery = null, string[] dashboardTags = null)
        {
            var parameters = new QueryStringParameters();

            if (dashboardIds != null && dashboardIds.Length > 0)
            {
                foreach (var dashboardId in dashboardIds)
                    parameters.Add("dashboardId", dashboardId);
            }

            if (panelId != null)
                parameters.Add(nameof(panelId), panelId);

            if (string.IsNullOrWhiteSpace(query) == false)
                parameters.Add(nameof(query), query);

            if (states != null && states.Length > 0)
            {
                foreach (var state in states)
                    parameters.Add("state", state.ToString());
            }

            if (limit != null)
                parameters.Add(nameof(limit), limit);

            if (folderIds != null && folderIds.Length > 0)
            {
                foreach (var folderId in folderIds)
                    parameters.Add("folderId", folderId.ToString());
            }

            if (string.IsNullOrWhiteSpace(dashboardQuery) == false)
                parameters.Add(nameof(dashboardQuery), dashboardQuery);

            if (dashboardTags != null && dashboardTags.Length > 0)
            {
                foreach (var dashboardTag in dashboardTags)
                    parameters.Add("dashboardTag", dashboardTag);
            }

            var response = await ExecuteGetRequestAsync<Alert[]>("/api/alerts", parameters, _authentication);

            return response;
        }

        public Alert GetAlert(int alertId)
        {
            var task = Task.Run(() => GetAlertAsync(alertId));

            task.Wait();

            return task.Result;
        }

        public async Task<Alert> GetAlertAsync(int alertId)
        {
            var response = await ExecuteGetRequestAsync<Alert>($"/api/alerts/{alertId}", null, _authentication);

            return response;
        }

        public PausedAlertResponse PauseAlert(int alertId, bool paused)
        {
            var task = Task.Run(() => PauseAlertAsync(alertId, paused));

            task.Wait();

            return task.Result;
        }

        public async Task<PausedAlertResponse> PauseAlertAsync(int alertId, bool paused)
        {
            var request = new { Paused = paused };

            var response = await ExecutePostRequestAsync<PausedAlertResponse, object>($"/api/alerts/{alertId}/pause", null, request, _authentication);

            return response;
        }

        public PausedAllAlertsResponse PauseAllAlerts(bool paused)
        {
            var task = Task.Run(() => PauseAllAlertsAsync(paused));

            task.Wait();

            return task.Result;
        }

        public async  Task<PausedAllAlertsResponse> PauseAllAlertsAsync(bool paused)
        {
            var request = new { Paused = paused };

            var response = await ExecutePostRequestAsync<PausedAllAlertsResponse, object>("/api/admin/pause-all-alerts", null, request, _authentication);

            return response;
        }
    }
}
