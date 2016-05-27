using SharpRedis.SerializeProvider;

namespace SharpRedis
{
    /// <summary>
    ///     链式配置
    /// </summary>
    public class Config
    {
        /// <summary>
        ///     设置序列化对象工厂
        /// </summary>
        /// <param name="factory"></param>
        /// <returns></returns>
        public Config SetSerializerFactory(ISerializerFactory factory)
        {
            Engine.SetSerializerFactory(factory);
            return this;
        }

        /// <summary>
        ///     设置连接字符串
        /// </summary>
        /// <param name="connectionString"></param>
        /// <returns></returns>
        public Config SetConnectionString(string connectionString)
        {
            Engine.SetConnectionString(connectionString);
            return this;
        }
    }
}