using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Runtime.InteropServices.ComTypes;
using StackExchange.Redis;
using FastReflection;
namespace SharpRedis
{
    public class Client
    {
        static public long GetSequenceId<T>()
        {
            var key = KeyFactory.GetSequenceKey<T>();

            return Engine.RedisDB.StringIncrement(key);
        }

        /// <summary>
        ///     使用Id手动创建统一资源名称
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="id"></param>
        /// <returns></returns>
        static public string CreateUrnKey<T>(object id)
        {
            return KeyFactory.GetUrnKey<T>(id);
        }

        /// <summary>
        ///     获取对象实例的统一资源名称
        /// </summary>
        /// <typeparam name="T">类型</typeparam>
        /// <param name="obj">类型实例</param>
        /// <returns></returns>
        static public string GetUrnKey<T>(T obj)
        {
            return GetUrnKey(obj, null);
        }

        /// <summary>
        ///     获取对象实例的统一资源名称
        /// </summary>
        /// <typeparam name="T">类型</typeparam>
        /// <param name="obj">类型实例</param>
        /// <param name="idFunc">指定获取Id的Func</param>
        /// <returns></returns>
        static public string GetUrnKey<T>(T obj, Func<T, object> idFunc)
        {
            var id = null == idFunc ? GetId(obj) : idFunc(obj);

            return KeyFactory.GetUrnKey<T>(id);
        }

        /// <summary>
        ///     
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="obj"></param>
        static public void Set<T>(T obj)
        {
            var urnKey = GetUrnKey(obj);

            Console.WriteLine(urnKey);
            
            var serializer = Engine.SerializerFactory.Create();
        }

        static public void Set(string key, object value)
        {
            
        }

        static public void SetList<T>(T[] list, Func<T, object> idExp, Func<T, long> incr = null)
        {
            if (null == list || !list.Any())
                return;

            var SequenceKey = KeyFactory.GetSequenceKey<T>();

            var serializer = Engine.SerializerFactory.Create();

            var kvs = list.ToDictionary(x => (RedisKey)KeyFactory.GetUrnKey<T>(idExp(x)), y => (RedisValue)serializer.Serialize(y));
            //var ids = list.Select(x => new SortedSetEntry(idExp(x).ToString(), Engine.RedisDB.StringIncrement(SequenceKey)));
            var ids = list.Select(x => new SortedSetEntry(idExp(x).ToString(), incr(x)));
            //var sequence = ids.Select(x => x.Score).ToList();
            //var ids = list.Select(x => (RedisValue)idExp(x).ToString());

            var idKey = KeyFactory.GetIdKey<T>();

            var trans = Engine.RedisDB.CreateTransaction();

            //trans.SetAddAsync(idKey, ids.ToArray());
            trans.SortedSetAddAsync(idKey, ids.ToArray());
            trans.StringSetAsync(kvs.ToArray());
            
            if(!trans.Execute())
                throw new ApplicationException("写入事务失败!");
        }

        static public T Get<T>(RedisKey key)
        {
            var value = Engine.RedisDB.StringGet(key);

            var serializer = Engine.SerializerFactory.Create();

            return serializer.Deserialize<T>(value);
        }

        static public PageList<T> GetList<T>(long pageIndex, int pageSize, bool orderByDesc = false)
        {
            if (pageIndex < 1)
                pageIndex = 1;

            if (pageSize < 1)
                pageSize = 1;

            long skip;

            var idKey = KeyFactory.GetIdKey<T>();

            var total = Engine.RedisDB.SortedSetLength(idKey);

            if(0 == total)
                return new PageList<T>(new T[]{}, 0, pageSize, 1);

            if (orderByDesc)
            {
                skip = total - (pageIndex*pageSize);
            }
            else
            {
                skip = (pageIndex - 1)*pageSize;
            }

            if(skip+pageSize > total || skip < 0)
                throw new ApplicationException("超出最大数据量范围！");

            var ids = Engine.RedisDB.SortedSetRangeByRank(idKey, skip, skip + pageSize - 1);

            var idKeys = ids.Select(x => (RedisKey) KeyFactory.GetUrnKey<T>(x)).ToArray();

            var jsonObjs =  Engine.RedisDB.StringGet(idKeys);

            var serializer = Engine.SerializerFactory.Create();

            var list = jsonObjs.Select(x => serializer.Deserialize<T>(x));

            if (orderByDesc)
                list = list.Reverse();

            return new PageList<T>(list, total, pageSize, pageIndex);
        }

        private static object GetId<T>(T obj)
        {
            const string idField = "Id";
            var type = typeof (T);
            return (from property in type.GetProperties() where string.Equals(idField, property.Name, StringComparison.OrdinalIgnoreCase) select property.FastGetValue(obj)).FirstOrDefault();
        }
    }
}