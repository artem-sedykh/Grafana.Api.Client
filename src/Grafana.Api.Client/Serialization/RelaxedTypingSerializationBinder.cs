using System;
using System.Collections.Concurrent;
using System.Linq;
#if NET45
using System.Runtime.Serialization;
#else
using Newtonsoft.Json;
#endif

namespace Grafana.Serialization
{
    public class RelaxedTypingSerializationBinder : SerializationBinder
    {
        private static readonly ConcurrentDictionary<string, Type> TypeMapCache = new ConcurrentDictionary<string, Type>();

        public override Type BindToType(string assemblyName, string typeName)
        {
            if (TypeMapCache.TryGetValue(typeName, out var type))
                return type;

            type = AppDomain.CurrentDomain.GetAssemblies().
                Where(a => !a.IsDynamic).
                SelectMany(a => a.GetExportedTypes()).
                FirstOrDefault(t => t.FullName != null && t.FullName.Equals(typeName));

            TypeMapCache.TryAdd(typeName, type);

            return type;
        }

        public override void BindToName(Type serializedType, out string assemblyName, out string typeName)
        {
            assemblyName = null;
            typeName = serializedType.FullName;
        }
    }
}
