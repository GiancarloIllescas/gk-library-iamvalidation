using System;
using System.Runtime.Caching;
using Yape.Library.IamValidation.Application.Ports;

namespace Yape.Library.IamValidation.Infrastructure.Adapters
{
    public sealed class CacheProvider : ICacheProvider
    {
        private readonly ObjectCache Cache = MemoryCache.Default;

        public T Get<T>(string key)
        {
            if (key == null) throw new ArgumentNullException(nameof(key));

            var obj = Cache.Get(key);

            return obj is T t ? t : default(T);
        }

        public bool Set<T>(string key, T obj, TimeSpan expirationTime)
        {
            if (key == null) throw new ArgumentNullException(nameof(key));

            var policy = new CacheItemPolicy
            {
                AbsoluteExpiration = DateTimeOffset.UtcNow.Add(expirationTime)
            };

            Cache.Set(key, obj, policy);

            return true;
        }
    }
}
