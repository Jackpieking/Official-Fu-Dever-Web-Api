using FuDever.App.Handler;
using FuDever.Application.Interfaces.Authentication.Jwt;
using Microsoft.Extensions.DependencyInjection;

namespace FuDever.App;

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
    public static void AddIdentityService(this IServiceCollection services)
    {
        services.ConfigureCore();
    }

    /// <summary>
    ///     Configure core services.
    /// </summary>
    /// <param name="services">
    ///     Service container.
    /// </param>
    private static void ConfigureCore(this IServiceCollection services)
    {
        services
            .AddScoped<IAccessTokenHandler, AccessTokenHandler>()
            .AddScoped<IRefreshTokenHandler, RefreshTokenHandler>();
    }
}
