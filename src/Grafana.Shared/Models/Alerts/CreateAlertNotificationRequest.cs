// ReSharper disable once CheckNamespace
namespace Grafana.Models
{
    public class CreateAlertNotificationRequest
    {
        public string Name { get; set; }

        public string Type { get; set; }

        public bool IsDefault { get; set; }

        public bool SendReminder { get; set; }

        public AlertNotificationSettings Settings { get; set; }
    }
}
