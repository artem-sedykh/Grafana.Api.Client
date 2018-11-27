// ReSharper disable once CheckNamespace
namespace Grafana.Models
{
    public class CreateAnnotationRequest: UpdateAnnotationRequest
    {
        public int? DashboardId { get; set; }

        public int? PanelId { get; set; }
    }
}