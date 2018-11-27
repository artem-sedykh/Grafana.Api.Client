// ReSharper disable once CheckNamespace
namespace Grafana.Models
{
    public enum AnnotationType
    {
        Annotation,
        Alert
    }

    public class Annotation
    {
        public int Id { get; set; }
        public int AlertId { get; set; }
        public int DashboardId { get; set; }
        public int PanelId { get; set; }
        public int UserId { get; set; }
        public string UserName { get; set; }
        public string NewState { get; set; }
        public string PrevState { get; set; }
        public long Time { get; set; }
        public string Text { get; set; }
        public string Metric { get; set; }
        public int RegionId { get; set; }
        public AnnotationType Type { get; set; }
        public string[] Tags { get; set; }
        public object Data { get; set; }
    }
}
