using Application.Interfaces.Caching;
using Application.Models;
using Microsoft.Extensions.Caching.Distributed;
using Newtonsoft.Json;
using System.Threading;
using System.Threading.Tasks;

namespace Cache.Handlers.Redis;

/// <summary>
///     Implementation of cache handler using redis as storage.
/// </summary>
internal sealed class RedisCacheHandler : ICacheHandler
{
    private readonly IDistributedCache _distributedCache;

    public RedisCacheHandler(IDistributedCache distributedCache)
    {
        _distributedCache = distributedCache;
    }

    public async Task<CacheModel<TSource>> GetAsync<TSource>(
        string key,
        CancellationToken cancellationToken)
    {
        var cachedValue = await _distributedCache.GetStringAsync(
            key: key,
            token: cancellationToken);

        if (string.IsNullOrWhiteSpace(value: cachedValue))
        {
            return CacheModel<TSource>.NotFound;
        }

        return JsonConvert.DeserializeObject<TSource>(value: cachedValue);
    }

    public Task RemoveAsync(
        string key,
        CancellationToken cancellationToken)
    {
        return _distributedCache.RemoveAsync(
            key: key,
            token: cancellationToken);
    }

    public Task SetAsync<TSource>(
        string key,
        TSource value,
        DistributedCacheEntryOptions distributedCacheEntryOptions,
        CancellationToken cancellationToken)
    {
        return _distributedCache.SetStringAsync(
            key: key,
            value: JsonConvert.SerializeObject(
                value: value,
                formatting: Formatting.None,
                settings: new JsonSerializerSettings()
                {
                    NullValueHandling = NullValueHandling.Ignore
                }),
            options: distributedCacheEntryOptions,
            token: cancellationToken);
    }
}
