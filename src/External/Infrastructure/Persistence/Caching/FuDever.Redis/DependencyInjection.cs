using FuDever.Application.Interfaces.Caching;
using FuDever.Configuration.Infrastructure.Cache.Redis;
using FuDever.Redis.Handler;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace FuDever.Redis;

/// <summary>
///     Configure services for this layer.
/// </summary>
public static class DependencyInjection
{
    /// <summary>
    ///     Entry to configuring multiple services.
    /// </summary>
    /// <param name="services">
    ///     Service container.
    /// </param>
    /// <param name="configuration">
    ///     Load configuration for configuration
    ///     file (appsetting).
    /// </param>
    public static void AddCachingDatabase(
        this IServiceCollection services,
        IConfigurationManager configuration)
    {
        services.ConfigureCore();

        services.ConfigureStackChangeRedis(configuration: configuration);
    }

    /// <summary>
    ///     Configure core services.
    /// </summary>
    /// <param name="services">
    ///     Service container.
    /// </param>
    private static void ConfigureCore(this IServiceCollection services)
    {
        services.AddScoped<ICacheHandler, RedisCacheHandler>();
    }

    /// <summary>
    ///     Configure stack change redis services.
    /// </summary>
    /// <param name="services">
    ///     Service container.
    /// </param>
    private static void ConfigureStackChangeRedis(
        this IServiceCollection services,
        IConfigurationManager configuration)
    {
        var redisOption = configuration
            .GetRequiredSection(key: "Cache")
            .GetRequiredSection(key: "Redis")
            .Get<RedisOption>();

        services.AddStackExchangeRedisCache(setupAction: config =>
        {
            config.Configuration = redisOption.ConnectionString;
        });
    }
}
