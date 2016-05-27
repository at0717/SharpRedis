using Jil;
using SharpRedis.SerializeProvider;

namespace SharpRedis.Jil
{
    public class Serializer : ISerializer
    {
        public string Serialize(object obj)
        {
            return JSON.Serialize(obj);
        }

        public T Deserialize<T>(string value)
        {
            return JSON.Deserialize<T>(value);
        }
    }
}