// ReSharper disable once CheckNamespace
namespace Grafana.Models
{
    public class CreateDashboardResponse
    {
        public int Id { get; set; }
        public string Uid { get; set; }
        public string Url { get; set; }
        public string Status { get; set; }
        public int Version { get; set; }
        public string Slug { get; set; }
    }
}