using SharpRedis.SerializeProvider;

namespace SharpRedis.JsonNet
{
    public class SerializerFactory : ISerializerFactory
    {
        public ISerializer Create()
        {
            return new Serializer();
        }
    }
}