namespace SharpRedis.SerializeProvider
{
    /// <summary>
    ///     序列化组件的抽象工厂
    /// </summary>
    public interface ISerializerFactory
    {
        /// <summary>
        ///     创建一个序列化组件
        /// </summary>
        /// <returns></returns>
        ISerializer Create();
    }
}