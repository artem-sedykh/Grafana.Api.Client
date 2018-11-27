using System;
using System.Diagnostics;
using System.Linq;
using System.Net;

namespace Grafana.Models
{
    internal class QueryString
    {
        public QueryStringParameters Parameters { get; }

        public QueryString() :
            this(new QueryStringParameters())
        {
        }

        [DebuggerStepThrough]
        public QueryString(QueryStringParameters parameters)
        {
            Parameters = parameters ?? throw new ArgumentNullException(nameof(parameters));
        }

        public string AppendToUrl(string url)
        {
            var queryString = ToString();

            if (url.EndsWith("?"))
                return url + queryString;

            if (url.Contains("?"))
            {
                return url.EndsWith("&") ?
                    url + queryString :
                    url + "&" + queryString;
            }

            return url + "?" + queryString;
        }

        public override string ToString()
        {
            var encodedParameters = Parameters.
                SelectMany(qs => qs.Values.Select(v => new { name = qs.Name, value = v })).
                Select(p => $"{p.name}={WebUtility.UrlEncode(p.value)}");

            var queryString = string.Join("&", encodedParameters);

            return queryString;
        }
    }
}
