using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Reflection.Serializer
{
    public interface Serializer
    {

        public string Serialize(ISerializable[] entities, string separator);
        public string Serialize(ISerializable entity, string separator);
        public List<T> DeserializeFromString<T>(string dataStr, string separator) where T : class, new();

    }
}
