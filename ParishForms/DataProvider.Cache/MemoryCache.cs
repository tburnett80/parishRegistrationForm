using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Threading.Tasks;
using ParishForms.Common.Contracts.DataProviders;
using ParishForms.Common.Extensions;

namespace DataProvider.Cache
{
    public sealed class MemoryCache : ICacheProvider
    {
        #region Constructor and Private members
        private readonly IDictionary<string, Container> _cache;

        public MemoryCache()
        {
            _cache = new ConcurrentDictionary<string, Container>();
        }
        #endregion

        #region Contract Impl
        public bool DataCached(string key)
        {
            if (string.IsNullOrEmpty(key.TryTrim()) || !_cache.ContainsKey(key))
                return false;

            if (!_cache[key].IsExpired)
                return _cache[key] != null;

            InvalidateKey(key);
            return false;
        }

        public bool InvalidateKey(string key)
        {
            if (!string.IsNullOrEmpty(key.TryTrim()) && _cache.ContainsKey(key))
                return _cache.Remove(key);

            return false;
        }

        public TEntity GetObjectFromCache<TEntity>(string key) where TEntity: class, new()
        {
            if (!DataCached(key) || !typeof(TEntity).SimpleTypeOf().Equals(_cache[key].Type))
                return null;

            return (TEntity) _cache[key].Data;
        }

        public async Task<bool> CacheObject<TEntity>(string key, TEntity obj, int ttlSeconds = 1800) where TEntity : class, new()
        {
            if(string.IsNullOrEmpty(key.TryTrim()) || obj == null || ttlSeconds < 5)
                return false;

            return await Task.Factory.StartNew(() =>
            {
                if (_cache.ContainsKey(key))
                    _cache[key] = new Container(ttlSeconds, obj, typeof(TEntity).SimpleTypeOf());
                else
                    _cache.Add(key, new Container(ttlSeconds, obj, typeof(TEntity).SimpleTypeOf()));

                return true;
            });
        }
        #endregion

        #region Private Methods and objects
        private class Container
        {
            public Container(int ttl, object data, string type)
            {
                ExpiresOn = DateTimeOffset.UtcNow.AddSeconds(ttl);
                Type = type;
                Data = data;
            }

            private DateTimeOffset ExpiresOn { get; }

            public string Type { get; }

            public bool IsExpired => 
                 DateTimeOffset.UtcNow >= ExpiresOn;

            public object Data { get; }
        }
        #endregion
    }
}
