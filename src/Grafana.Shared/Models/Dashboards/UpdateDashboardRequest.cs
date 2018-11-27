// ReSharper disable once CheckNamespace
namespace Grafana.Models
{
    public class UpdateDashboardRequest
    {
        /// <summary>
        /// The complete dashboard model
        /// </summary>
        public Dashboard Dashboard { get; set; }

        /// <summary>
        /// The id of the folder to save the dashboard in.
        /// </summary>
        public int FolderId { get; set; }

        /// <summary>
        /// Set to true if you want to overwrite existing dashboard with newer version, same dashboard title in folder or same dashboard uid.
        /// </summary>
        public bool Overwrite { get; set; }
    }
}
