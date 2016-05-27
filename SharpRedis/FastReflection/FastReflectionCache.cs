using System.Collections.Generic;

namespace FastReflection
{
    public abstract class FastReflectionCache<TKey, TValue> : IFastReflectionCache<TKey, TValue>
    {
        private readonly Dictionary<TKey, TValue> _cache = new Dictionary<TKey, TValue>();

        public TValue Get(TKey key)
        {
            var value = default(TValue);
            if (_cache.TryGetValue(key, out value))
            {
                return value;
            }

            lock (key)
            {
                if (!_cache.TryGetValue(key, out value))
                {
                    value = Create(key);
                    _cache[key] = value;
                }
            }

            return value;
        }

        protected abstract TValue Create(TKey key);
    }
}