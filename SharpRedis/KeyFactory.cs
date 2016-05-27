namespace SharpRedis
{
    internal class KeyFactory
    {
        /// <summary>
        ///     获取类型的统一资源名称前缀
        /// <para>前缀+Id构成类型唯一key</para>
        /// <para>urn:uniform resource name</para>
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        static public string GetUrnKeyPerfix<T>()
        {
            return string.Format("urn:{0}:", GetTypeName<T>());
        }

        /// <summary>
        ///     获取类型的Id的key
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        static public string GetIdKey<T>()
        {
            return string.Concat("ids:", GetTypeName<T>());
        }

        /// <summary>
        ///     获取类型序列号的key
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        static public string GetSequenceKey<T>()
        {
            return string.Concat("seq:", GetTypeName<T>());
        }

        /// <summary>
        ///     
        /// </summary>
        /// <typeparam name="T">类型</typeparam>
        /// <typeparam name="TId">Id类型</typeparam>
        /// <param name="id"></param>
        /// <returns></returns>
        static public string GetUrnKey<T>(object id)
        {
            var perfix = GetUrnKeyPerfix<T>();
            return string.Concat(perfix, id);
        }

        static private string GetTypeName<T>()
        {
            var type = typeof(T);

            return type.FullName.Replace(".", ":");
        }
    }
}