// ReSharper disable once CheckNamespace
namespace Grafana.Models
{
    public class UpdateAlertNotificationResponse
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Type { get; set; }
        public bool IsDefault { get; set; }
        public bool SendReminder { get; set; }
        public string Frequency { get; set; }
        public AlertNotificationSettings Settings { get; set; }
        public string Created { get; set; }
        public string Updated { get; set; }
    }
}
