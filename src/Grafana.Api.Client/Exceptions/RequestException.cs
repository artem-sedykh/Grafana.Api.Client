using System;

namespace Grafana.Exceptions
{
    public class RequestException : Exception
    {
        public int StatusCode { get; }

        public string ReasonPhrase { get; }

        public string Response { get; }

        public RequestException(string uri, int statusCode, string reasonPhrase, string response) : base($"Address: {uri}, HttpCode: {statusCode}, ReasonPhrase: {reasonPhrase}")
        {
            StatusCode = statusCode;

            ReasonPhrase = reasonPhrase;

            Response = response;
        }
    }
}
