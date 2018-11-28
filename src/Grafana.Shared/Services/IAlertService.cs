using System.Threading.Tasks;
using Grafana.Models;

namespace Grafana.Services
{
    public interface IAlertService
    {
        /// <summary>
        /// <see cref="http://docs.grafana.org/http_api/alerting/"/>
        /// </summary>
        Alert[] GetAlerts(int[] dashboardIds = null, int? panelId = null, string query = null, AlertState[] states = null, int? limit = null, int[] folderId = null, string dashboardQuery = null, string[] dashboardTag = null);

        /// <summary>
        /// <see cref="http://docs.grafana.org/http_api/alerting/"/>
        /// </summary>
        Task<Alert[]> GetAlertsAsync(int[] dashboardIds = null, int? panelId = null, string query = null, AlertState[] states = null, int? limit = null, int[] folderId = null, string dashboardQuery = null, string[] dashboardTag = null);

        /// <summary>
        /// <see cref="http://docs.grafana.org/http_api/alerting/"/>
        /// </summary>
        Alert GetAlert(int alertId);

        /// <summary>
        /// <see cref="http://docs.grafana.org/http_api/alerting/"/>
        /// </summary>
        Task<Alert> GetAlertAsync(int alertId);

        /// <summary>
        /// <see cref="http://docs.grafana.org/http_api/alerting/"/>
        /// </summary>
        PausedAlertResponse PauseAlert(int alertId, bool paused);

        /// <summary>
        /// <see cref="http://docs.grafana.org/http_api/alerting/"/>
        /// </summary>
        Task<PausedAlertResponse> PauseAlertAsync(int alertId, bool paused);

        /// <summary>
        /// <see cref="http://docs.grafana.org/http_api/alerting/"/>
        /// </summary>
        PausedAllAlertsResponse PauseAllAlerts(bool paused);

        /// <summary>
        /// <see cref="http://docs.grafana.org/http_api/alerting/"/>
        /// </summary>
        Task<PausedAllAlertsResponse> PauseAllAlertsAsync(bool paused);
    }
}
