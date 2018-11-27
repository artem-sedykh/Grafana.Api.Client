// ReSharper disable once CheckNamespace
namespace Grafana.Models
{
    public class UpdateAnnotationRequest
    {
        /// <summary>
        /// Epoch datetime in milliseconds
        /// </summary>
        public long Time { get; set; }

        public bool IsRegion { get; set; }

        /// <summary>
        /// Epoch datetime in milliseconds
        /// </summary>
        public long? TimeEnd { get; set; }

        public string[] Tags { get; set; }

        public string Text { get; set; }
    }
}