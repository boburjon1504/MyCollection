using MyCollection.Domain.Common.Settings;
namespace MyCollection.Persistence.Brokers.Interfaces;
public interface ICacheBroker
{
    ValueTask<T?> GetAsync<T>(string key);

    ValueTask<bool> TryGetAsync<T>(string key, out T? value);

    ValueTask<T?> GetOrSetAsync<T>(string key, Func<Task<T>> valueFactory);

    ValueTask SetAsync<T>(string key, T value);

    ValueTask DeleteAsync<T>(string key);
}
