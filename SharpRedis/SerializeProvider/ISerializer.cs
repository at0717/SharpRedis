namespace SharpRedis.SerializeProvider
{
    /// <summary>
    ///     序列化组件接口
    /// </summary>
    public interface ISerializer
    {
        /// <summary>
        ///     序列化对象
        /// </summary>
        /// <param name="obj">需要序列化的对象</param>
        /// <returns></returns>
        string Serialize(object obj);

        /// <summary>
        ///     反序列化对象
        /// </summary>
        /// <typeparam name="T">对象类型</typeparam>
        /// <param name="value">需要反序列化的对象值</param>
        /// <returns></returns>
        T Deserialize<T>(string value);
    }
}