using System;

// ReSharper disable once CheckNamespace
namespace Grafana.Models
{
    public class CreateAlertNotificationResponse
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Type { get; set; }
        public bool IsDefault { get; set; }
        public bool SendReminder { get; set; }
        public AlertNotificationSettings Settings { get; set; }
        public DateTime Created { get; set; }
        public DateTime Updated { get; set; }
    }
}
