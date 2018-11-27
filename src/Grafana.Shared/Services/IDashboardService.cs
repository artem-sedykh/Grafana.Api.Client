using Grafana.Models;

namespace Grafana.Services
{
    public interface IDashboardService
    {
        CreateDashboardResponse Create(CreateDashboardRequest request);

        UpdateDashboardResponse Update(UpdateDashboardRequest request);

        /// <summary>
        /// Get dashboard by uid
        /// </summary>
        /// <param name="uId">dashboard unique identifier</param>
        /// <returns>Will return the dashboard given the dashboard unique identifier (uid).</returns>
        DashboardResponse GetDashboardByUid(string uId);

        /// <summary>
        /// Get dashboard by slug, starting from Grafana v5.0
        /// </summary>
        /// <param name="slug">Url friendly version of the dashboard title</param>
        /// <returns>Will return the dashboard given the dashboard slug. Slug is the url friendly version of the dashboard title. If there exists multiple dashboards with the same slug, one of them will be returned in the response.</returns>
        DashboardResponse GetDashboardBySlug(string slug);

        /// <summary>
        /// Delete the dashboard given the specified unique identifier (uid).
        /// </summary>
        /// <param name="uId">unique identifier (uid)</param>
        void DeleteDashboardByUid(string uId);

        /// <summary>
        /// Delete the dashboard given the specified slug. Slug is the url friendly version of the dashboard title.
        /// </summary>
        /// <param name="slug">Url friendly version of the dashboard title</param>
        void DeleteDashboardBySlug(string slug);
    }
}
