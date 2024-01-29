using Application.Interfaces.Caching;
using Cache.Handlers.Redis;
using Configuration.Infrastructure.Cache;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Cache;

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
    public static void AddCache(
        this IServiceCollection services,
        IConfigurationManager configuration)
    {
        services.ConfigureStackChangeRedis(configuration: configuration);

        services.ConfigureCore();
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
        const string CacheSection = "Cache";
        const string RedisSection = "Redis";

        var redisOption = configuration
            .GetRequiredSection(key: CacheSection)
            .GetRequiredSection(key: RedisSection)
            .Get<RedisOption>();

        services.AddStackExchangeRedisCache(setupAction: config =>
        {
            config.Configuration = redisOption.ConnectionString;
        });
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
}
