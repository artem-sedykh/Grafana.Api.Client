using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace Grafana.Models
{
    public class QueryStringParameter
    {
        public string Name { get; }

        public ReadOnlyCollection<string> Values { get; }

        public QueryStringParameter(string name, IEnumerable<string> values)
        {
            Name = name;
            Values = new ReadOnlyCollection<string>(values.ToList());
        }
    }
}