using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Diagnostics;
using System.Globalization;
using System.Linq;

namespace Grafana.Models
{
    [DebuggerStepThrough]
    public class QueryStringParameters : IEnumerable<QueryStringParameter>
    {
        private readonly NameValueCollection _queryString;

        public QueryStringParameters()
        {
            _queryString = new NameValueCollection();
        }

        public void Add(string name, IFormattable value)
        {
            if (value == null) return;

            Add(name, value.ToString(null, CultureInfo.InvariantCulture));
        }

        public void Add(string name, string value)
        {
            if (value == null) return;

            _queryString.Add(name, value);
        }

        public IEnumerator<QueryStringParameter> GetEnumerator()
        {
            return _queryString.AllKeys.Select(k => new QueryStringParameter(k, _queryString.GetValues(k))).GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
