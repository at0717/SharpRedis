using SharpRedis.SerializeProvider;

namespace SharpRedis.Jil
{
    public class SerializerFactory : ISerializerFactory
    {
        public ISerializer Create()
        {
            return new Serializer();
        }
    }
}