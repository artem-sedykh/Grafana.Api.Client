// ReSharper disable once CheckNamespace
namespace Grafana.Models
{
    public class PausedAlertResponse : MessageResponse
    {
        public int AlertId { get; set; }

        public string State { get; set; }
    }
}