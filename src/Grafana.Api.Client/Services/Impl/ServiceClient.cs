using System;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Grafana.Exceptions;
using Grafana.Models;
using Grafana.Serialization;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Serialization;

namespace Grafana.Services.Impl
{
    public abstract class ServiceClient : IDisposable
    {
        protected string MediaType = "application/json";

        /// <summary>
        /// Название и версия клиентского приложения, обращающегося к сервису
        /// </summary>
        private readonly string _applicationName;

        protected virtual Uri BaseUrl { get; }

        /// <summary>
        ///HttpClient is intended to be instantiated once and reused throughout the life of an application. The following conditions can result in SocketException errors:
        ///Creating a new HttpClient instance per request.
        ///Server under heavy load.
        ///Creating a new HttpClient instance per request can exhaust the available sockets.
        /// </summary>
        private readonly HttpClient _httpClient;

        #region .ctor

        protected ServiceClient(Uri baseUrl)
        {
            BaseUrl = baseUrl ?? throw new ArgumentNullException(nameof(baseUrl));

            _applicationName = "grafana-api-client";

            // ReSharper disable once VirtualMemberCallInConstructor
            _httpClient = CreateHttpClient();
        }

        #endregion

        #region HTTP запросы

        #region GET

        protected virtual T ExecuteGetRequest<T>(string relativeUrl, QueryStringParameters parameters = null, IAuthenticationData authentication = null)
        {
            var task = Task.Run(() => ExecuteGetRequestAsync<T>(relativeUrl, parameters, authentication));

            task.Wait();

            return task.Result;
        }

        protected virtual async Task<T> ExecuteGetRequestAsync<T>(string relativeUrl, QueryStringParameters parameters = null, IAuthenticationData authentication = null)
        {
            return await ExecuteRequestAsync<T, object>(HttpMethod.Get, relativeUrl, parameters, null, authentication);
        }

        #endregion

        #region POST

        protected virtual T ExecutePostRequest<T, TBody>(string relativeUrl, QueryStringParameters parameters = null, TBody body = null, IAuthenticationData authentication = null) where TBody : class
        {
            var task = Task.Run(() => ExecutePostRequestAsync<T, TBody>(relativeUrl, parameters, body, authentication));

            task.Wait();

            return task.Result;
        }

        protected virtual async Task<T> ExecutePostRequestAsync<T, TBody>(string relativeUrl, QueryStringParameters parameters = null, TBody body = null, IAuthenticationData authentication = null)
            where TBody : class
        {
            return await ExecuteRequestAsync<T, TBody>(HttpMethod.Post, relativeUrl, parameters, body, authentication);
        }

        #endregion

        #region PUT

        protected virtual T ExecutePutRequest<T, TBody>(string relativeUrl, QueryStringParameters parameters = null, TBody body = null, IAuthenticationData authentication = null)
            where TBody : class
        {
            var task = Task.Run(() => ExecutePutRequestAsync<T, TBody>(relativeUrl, parameters, body, authentication));

            task.Wait();

            return task.Result;
        }

        protected virtual async Task<T> ExecutePutRequestAsync<T, TBody>(string relativeUrl, QueryStringParameters parameters = null, TBody body = null, IAuthenticationData authentication = null)
            where TBody : class
        {
            return await ExecuteRequestAsync<T, TBody>(HttpMethod.Put, relativeUrl, parameters, body, authentication);
        }

        #endregion

        #region DELETE

        protected virtual T ExecuteDeleteRequest<T, TBody>(string relativeUrl, QueryStringParameters parameters = null, TBody body = null, IAuthenticationData authentication = null) where TBody : class
        {
            var task = Task.Run(() => ExecuteDeleteRequestAsync<T, TBody>(relativeUrl, parameters, body, authentication));

            task.Wait();

            return task.Result;
        }

        protected virtual async Task<T> ExecuteDeleteRequestAsync<T, TBody>(string relativeUrl, QueryStringParameters parameters = null, TBody body = null, IAuthenticationData authentication = null) where TBody : class
        {
            return await ExecuteRequestAsync<T, TBody>(HttpMethod.Delete, relativeUrl, parameters, body, authentication);
        }

        #endregion

        #region PATCH

        protected virtual T ExecutePatchRequest<T, TBody>(string relativeUrl, QueryStringParameters parameters = null, TBody body = null, IAuthenticationData authentication = null) where TBody : class
        {
            var task = Task.Run(() => ExecutePatchRequestAsync<T, TBody>(relativeUrl, parameters, body, authentication));

            task.Wait();

            return task.Result;
        }

        protected virtual async Task<T> ExecutePatchRequestAsync<T, TBody>(string relativeUrl, QueryStringParameters parameters = null, TBody body = null, IAuthenticationData authentication = null)
            where TBody : class
        {
            return await ExecuteRequestAsync<T, TBody>(new HttpMethod("PATCH"), relativeUrl, parameters, body, authentication);
        }

        #endregion

        protected virtual async Task<T> ExecuteRequestAsync<T, TBody>(HttpMethod httpMethod, string relativeUrl, QueryStringParameters parameters = null, TBody body = null, IAuthenticationData authentication = null)
            where TBody : class
        {
            var uri = CreateActionUri(relativeUrl, parameters);

            var requestMessage = CreateHttpRequestMessage(httpMethod, uri, authentication);

            if (body != null)
                requestMessage.Content = CreateStringContent(body);

            using (var response = await _httpClient.SendAsync(requestMessage, HttpCompletionOption.ResponseContentRead))
            {
                var responseStream = await response.Content.ReadAsStreamAsync();

                using (var jsonTextReader = new JsonTextReader(new StreamReader(responseStream, Encoding.UTF8)))
                {
                    var jsonSerializer = CreateJsonSerializer();

                    if (response.IsSuccessStatusCode)
                    {
                        var result = jsonSerializer.Deserialize<T>(jsonTextReader);

                        return result;
                    }

                    ExceptionHandler(jsonSerializer, jsonTextReader, uri, response.StatusCode, response.ReasonPhrase);

                    return default(T);
                }
            }
        }

        #endregion

        protected virtual void ExceptionHandler(JsonSerializer jsonSerializer, JsonReader jsonReader, Uri requestUri, HttpStatusCode httpStatusCode, string reasonPhrase)
        {
            string response = null;

            try
            {
                response = jsonSerializer.Deserialize(jsonReader)?.ToString();
            }
            catch
            {
                // ignored
            }

            throw new RequestException(requestUri.ToString(), (int) httpStatusCode, reasonPhrase, response);
        }

        protected virtual Uri CreateActionUri(string relativeUrl, QueryStringParameters parameters = null)
        {
            if (parameters != null)
            {
                var queryString = new QueryString(parameters);
                relativeUrl = queryString.AppendToUrl(relativeUrl);
            }

            // См. http://stackoverflow.com/a/23438417/60188
            relativeUrl = relativeUrl.TrimStart('/');

            var url = new Uri($"{BaseUrl.ToString().TrimEnd('/')}/{relativeUrl}");

            return url;
        }

        /// <summary>
        /// Создание сериализатора
        /// </summary>
        /// <returns></returns>
        protected virtual JsonSerializer CreateJsonSerializer()
        {
            var jsonSerializer = new JsonSerializer
            {
                ContractResolver = new CamelCasePropertyNamesContractResolver(),
                TypeNameHandling = TypeNameHandling.Auto,
                Binder = new RelaxedTypingSerializationBinder()
            };

            jsonSerializer.Converters.Add(new StringEnumConverter());
            jsonSerializer.Converters.Add(new IsoDateTimeConverter());
            jsonSerializer.Formatting = Formatting.Indented;
            return jsonSerializer;
        }

        protected virtual HttpRequestMessage CreateHttpRequestMessage(HttpMethod httpMethod, Uri uri, IAuthenticationData authentication = null)
        {
            var requestMessage = new HttpRequestMessage(httpMethod, uri);

            requestMessage.Headers.UserAgent.Clear();

            requestMessage.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue(MediaType));

            if (authentication != null)
                requestMessage.Headers.Authorization = new AuthenticationHeaderValue(authentication.Scheme, authentication.Parameter);

            return requestMessage;
        }

        protected virtual StringContent CreateStringContent<TBody>(TBody body = null) where TBody : class
        {
            var jsonBuilder = new StringBuilder();

            using (var jsonTextWriter = new JsonTextWriter(new StringWriter(jsonBuilder)))
            {
                var jsonSerializer = CreateJsonSerializer();

                jsonSerializer.Serialize(jsonTextWriter, body);

                var result = new StringContent(jsonBuilder.ToString(), Encoding.UTF8, MediaType);

                return result;
            }
        }

        protected virtual HttpClient CreateHttpClient()
        {
            var httpClient = new HttpClient { BaseAddress = BaseUrl };

            httpClient.DefaultRequestHeaders.UserAgent.ParseAdd(_applicationName);

            return httpClient;
        }

        public void Dispose()
        {
            _httpClient?.Dispose();
        }
    }
}
