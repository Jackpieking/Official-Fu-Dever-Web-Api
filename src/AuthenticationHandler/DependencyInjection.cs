using Application.Interfaces.Authentication.Jwt;
using AuthenticationHandler.Handler.Jwt;
using Generator.Handler.Token;
using Microsoft.Extensions.DependencyInjection;

namespace AuthenticationHandler;

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
    public static void AddAuthenticationHandler(this IServiceCollection services)
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
        services.AddScoped<IAccessTokenHandler, AccessTokenHandler>();
        services.AddScoped<IRefreshTokenHandler, RefreshTokenHandler>();
    }
}
