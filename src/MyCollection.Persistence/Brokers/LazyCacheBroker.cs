using LazyCache;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Options;
using MyCollection.Domain.Common.Settings;
using MyCollection.Persistence.Brokers.Interfaces;

namespace MyCollection.Persistence.Brokers;
public class LazyCacheBroker(IAppCache appCache,IOptions<CacheSettings> cacheSettings) : ICacheBroker
{

    private readonly MemoryCacheEntryOptions _cacheSettings = new MemoryCacheEntryOptions()
    {
        AbsoluteExpirationRelativeToNow = TimeSpan.FromSeconds(cacheSettings.Value.AbsoluteExpirationTimeInSeconds),
        SlidingExpiration = TimeSpan.FromSeconds(cacheSettings.Value.SlidingExpirationTimeInSeconds),
    };

    public async  ValueTask<T?> GetAsync<T>(string key)
    {
        return await appCache.GetAsync<T>(key);
    }

    public async ValueTask<T?> GetOrSetAsync<T>(string key, Func<Task<T>> valueFactory)
    {
        return await appCache.GetOrAddAsync(key, valueFactory, _cacheSettings);
    }

    public ValueTask SetAsync<T>(string key, T item)
    {
        appCache.Add<T>(key, item, _cacheSettings);

        return ValueTask.CompletedTask;
    }

    public ValueTask<bool> TryGetAsync<T>(string key, out T? value)
    {
        return new(appCache.TryGetValue(key, out value));
    }

    public ValueTask DeleteAsync<T>(string key)
    {
        appCache.Remove(key);
        return ValueTask.CompletedTask;
    }
}
