using System;

// ReSharper disable once CheckNamespace
namespace Grafana.Models
{
    public class Alert
    {
        public int Id { get; set; }
        public int DashboardId { get; set; }
        public string DashboardUId { get; set; }
        public string DashboardSlug { get; set; }
        public int PanelId { get; set; }
        public string Name { get; set; }
        public string State { get; set; }
        public DateTime NewStateDate { get; set; }
        public DateTime EvalDate { get; set; }
        public object EvalData { get; set; }

        public object EvalMatches { get; set; }

        public string ExecutionError { get; set; }

        public string Url { get; set; }
    }
}
