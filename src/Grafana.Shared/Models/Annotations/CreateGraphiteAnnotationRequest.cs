// ReSharper disable once CheckNamespace
namespace Grafana.Models
{
    public class CreateGraphiteAnnotationRequest
    {
        public string What { get; set; }

        public string[] Tags { get; set; }

        public long When { get; set; }

        public string Data { get; set; }
    }
}