# Grafana.Api.Client  [![NuGet](https://img.shields.io/nuget/v/Grafana.Api.Client.svg)](https://www.nuget.org/packages/Grafana.Api.Client/) [![Downloads](https://img.shields.io/nuget/dt/Grafana.Api.Client.svg)](https://www.nuget.org/packages/Grafana.Api.Client/) 

=============================================
## Status
[![Build status](https://ci.appveyor.com/api/projects/status/g85k3gptp673wig8/branch/release?svg=true)](https://ci.appveyor.com/project/artem-sedykh/grafana-api-client/branch/release)

C# Grafana api client

## Native grafana annotations

![Annotations](http://docs.grafana.org/img/docs/v46/annotations.png)

```csharp
var uri = new Uri("http://localhost:3000/");

//grafana configurations /org/apikeys
var apiKey = "....";

var annotationService = new AnnotationService(uri, apiKey);

var dashboardService = new DashboardService(uri, apiKey);

//Uid may be edited in dashboard settings
var dashboardResponse = dashboardService.GetDashboardByUid("test_dashboard");

var dashboardId = dashboardResponse.Dashboard.Id;

//Epoch datetime in milliseconds
var start = DateTime.UtcNow.AddMinutes(-15).ToGrafanaTimestamp();

//Epoch datetime in milliseconds
var stop = DateTime.UtcNow.ToGrafanaTimestamp();

//create annotation 
annotationService.Create(new CreateAnnotationRequest
{
    DashboardId = dashboardId,
    IsRegion = true,
    Tags = new[] {"deploy", Environment.MachineName },
    Text = $"Deploy on {Environment.MachineName}",
    Time = start,
    TimeEnd = stop
});
```
<br/>

#### adding grafana api key

![ApiKey](http://docs.grafana.org/img/docs/v46/annotations.png)

#### set dashboard uid

![ApiKey](http://docs.grafana.org/img/docs/v46/annotations.png)