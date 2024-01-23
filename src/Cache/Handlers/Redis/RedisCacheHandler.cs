using Application.Interfaces.Caching;
using System.Threading;
using System.Threading.Tasks;

namespace Cache.Handlers.Redis;

internal sealed class RedisCacheHandler : ICacheHandler
{
    public Task<TSource> GetAsync<TSource>(string key, CancellationToken cancellationToken) where TSource : class
    {
        throw new System.NotImplementedException();
    }

    public Task<bool> RemoveAsync(string key, CancellationToken cancellationToken)
    {
        throw new System.NotImplementedException();
    }

    public Task<bool> RemoveByPrefixAsync(string prefixKey, CancellationToken cancellationToken)
    {
        throw new System.NotImplementedException();
    }

    public Task SetAsync<TSource>(string key, TSource value, CancellationToken cancellationToken) where TSource : class
    {
        throw new System.NotImplementedException();
    }
}
