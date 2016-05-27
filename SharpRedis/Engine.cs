using System;
using SharpRedis.SerializeProvider;
using StackExchange.Redis;

namespace SharpRedis
{
    /// <summary>
    ///     组件引擎
    /// </summary>
    static internal class Engine
    {
        static Engine()
        {
            ConnectionString = "localhost";
        }
        
        static private string ConnectionString { get; set; }

        static private readonly Lazy<ConnectionMultiplexer> Connection = new Lazy<ConnectionMultiplexer>(
                () => ConnectionMultiplexer.Connect(ConnectionString));

        /// <summary>
        ///     获取序列化组件工厂
        /// </summary>
        static public ISerializerFactory SerializerFactory { get; private set; }

        /// <summary>
        ///     设置序列化组件工厂
        /// </summary>
        /// <param name="factory"></param>
        static public void SetSerializerFactory(ISerializerFactory factory)
        {
            SerializerFactory = factory;
        }

        /// <summary>
        ///     设置连接字符串
        /// </summary>
        /// <param name="connectionString"></param>
        static public void SetConnectionString(string connectionString)
        {
            ConnectionString = connectionString;
        }

        /// <summary>
        ///     获取Redis内部DB的连接对象
        /// </summary>
        static public IDatabase RedisDB
        {
            get
            {
                return Connection.Value.GetDatabase();
            }
        }
    }
}