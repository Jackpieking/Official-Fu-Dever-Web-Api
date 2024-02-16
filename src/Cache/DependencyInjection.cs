using Application.Interfaces.Caching;
using Cache.Handlers.Redis;
using Configuration.Infrastructure.Cache.Redis;
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
        var redisOption = configuration
            .GetRequiredSection(key: "Cache")
            .GetRequiredSection(key: "Redis")
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
