// ReSharper disable once CheckNamespace
namespace Grafana.Models
{
    public class Dashboard
    {
        /// <summary>
        ///  id = null to create a new dashboard.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Optional unique identifier when creating a dashboard. uid = null will generate a new uid.
        /// </summary>
        public string Uid { get; set; }

        public string Title { get; set; }

        public string[] Tags { get; set; }

        public string Timezone { get; set; }

        public int SchemaVersion { get; set; }

        public int Version { get; set; }
    }
}
