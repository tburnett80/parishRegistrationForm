using System.Threading.Tasks;

namespace ParishForms.Common.Contracts.DataProviders
{
    public interface ICacheProvider
    {
        bool DataCached(string key);

        bool InvalidateKey(string key);

        TEntity GetObjectFromCache<TEntity>(string key) where TEntity : class, new();

        Task<bool> CacheObject<TEntity>(string key, TEntity obj, int ttlSeconds = 1800) where TEntity : class, new();
    }
}
