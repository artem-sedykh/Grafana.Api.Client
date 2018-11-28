using System.Threading.Tasks;
using Grafana.Models;

namespace Grafana.Services
{
    public interface IAnnotationService
    {

        /// <summary>
        /// Find Annotations
        /// </summary>
        /// <param name="from">epoch datetime in milliseconds. Optional.</param>
        /// <param name="to">epoch datetime in milliseconds. Optional.</param>
        /// <param name="limit">Optional - default is 100. Max limit for results returned.</param>
        /// <param name="alertId">Optional. Find annotations for a specified alert.</param>
        /// <param name="dashboardId">Optional. Find annotations that are scoped to a specific dashboard</param>
        /// <param name="panelId">Optional. Find annotations that are scoped to a specific panel</param>
        /// <param name="userId">Optional. Find annotations created by a specific user</param>
        /// <param name="type">Optional. alert|annotation Return alerts or user created annotations</param>
        /// <param name="tags">Optional. Use this to filter global annotations. Global annotations are annotations from an annotation data source that are not connected specifically to a dashboard or panel. </param>
        Annotation[] FindAnnotations(long? from = null, long? to = null, int limit = 100, int? alertId = null,
            int? dashboardId = null, int? panelId = null, int? userId = null, AnnotationType? type = null,
            string[] tags = null);

        /// <summary>
        /// Find Annotations
        /// </summary>
        /// <param name="from">epoch datetime in milliseconds. Optional.</param>
        /// <param name="to">epoch datetime in milliseconds. Optional.</param>
        /// <param name="limit">Optional - default is 100. Max limit for results returned.</param>
        /// <param name="alertId">Optional. Find annotations for a specified alert.</param>
        /// <param name="dashboardId">Optional. Find annotations that are scoped to a specific dashboard</param>
        /// <param name="panelId">Optional. Find annotations that are scoped to a specific panel</param>
        /// <param name="userId">Optional. Find annotations created by a specific user</param>
        /// <param name="type">Optional. alert|annotation Return alerts or user created annotations</param>
        /// <param name="tags">Optional. Use this to filter global annotations. Global annotations are annotations from an annotation data source that are not connected specifically to a dashboard or panel. </param>
        Task<Annotation[]> FindAnnotationsAsync(long? from = null, long? to = null, int limit = 100, int? alertId = null,
            int? dashboardId = null, int? panelId = null, int? userId = null, AnnotationType? type = null,
            string[] tags = null);

        /// <summary>
        /// Creates an annotation in the Grafana database. The dashboardId and panelId fields are optional. If they are not specified then a global annotation is created and can be queried in any dashboard that adds the Grafana annotations data source. When creating a region annotation the response will include both id and endId, if not only id.
        /// </summary>
        CreateMessageResponse Create(CreateAnnotationRequest request);

        /// <summary>
        /// Creates an annotation in the Grafana database. The dashboardId and panelId fields are optional. If they are not specified then a global annotation is created and can be queried in any dashboard that adds the Grafana annotations data source. When creating a region annotation the response will include both id and endId, if not only id.
        /// </summary>
        Task<CreateMessageResponse> CreateAsync(CreateAnnotationRequest request);

        /// <summary>
        /// Creates an annotation by using Graphite-compatible event format. The when and data fields are optional. If when is not specified then the current time will be used as annotation’s timestamp. The tags field can also be in prior to Graphite 0.10.0 format (string with multiple tags being separated by a space).
        /// </summary>
        CreateGraphiteMessageResponse Create(CreateGraphiteAnnotationRequest request);

        /// <summary>
        /// Creates an annotation by using Graphite-compatible event format. The when and data fields are optional. If when is not specified then the current time will be used as annotation’s timestamp. The tags field can also be in prior to Graphite 0.10.0 format (string with multiple tags being separated by a space).
        /// </summary>
        Task<CreateGraphiteMessageResponse> CreateAsync(CreateGraphiteAnnotationRequest request);

        /// <summary>
        /// Update Annotation
        /// </summary>
        MessageResponse Update(int annotationId, UpdateAnnotationRequest request);

        /// <summary>
        /// Update Annotation
        /// </summary>
        Task<MessageResponse> UpdateAsync(int annotationId, UpdateAnnotationRequest request);

        /// <summary>
        /// Deletes the annotation that matches the specified id.
        /// </summary>
        MessageResponse Delete(int annotationId);

        /// <summary>
        /// Deletes the annotation that matches the specified id.
        /// </summary>
        Task<MessageResponse> DeleteAsync(int annotationId);

        /// <summary>
        /// Deletes the annotation that matches the specified region id. A region is an annotation that covers a timerange and has a start and end time. In the Grafana database, this is a stored as two annotations connected by a region id.
        /// </summary>
        MessageResponse DeleteByRegionId(int regionId);

        /// <summary>
        /// Deletes the annotation that matches the specified region id. A region is an annotation that covers a timerange and has a start and end time. In the Grafana database, this is a stored as two annotations connected by a region id.
        /// </summary>
        Task<MessageResponse> DeleteByRegionIdAsync(int regionId);
    }
}
