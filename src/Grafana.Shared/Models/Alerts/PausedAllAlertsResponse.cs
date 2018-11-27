// ReSharper disable once CheckNamespace
namespace Grafana.Models
{
    public class PausedAllAlertsResponse : MessageResponse
    {
        public string State { get; set; }

        public int AlertsAffected { get; set; }
    }
}