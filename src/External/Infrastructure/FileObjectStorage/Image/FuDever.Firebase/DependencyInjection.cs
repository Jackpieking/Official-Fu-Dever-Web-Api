using FuDever.Application.Interfaces.ExternalFiles;
using FuDever.Firebase.Image;
using Microsoft.Extensions.DependencyInjection;

namespace FuDever.Firebase;

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
    public static void AddFileObjectStorage(this IServiceCollection services)
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
        services.AddScoped<IDefaultUserAvatarAsUrlHandler, DefaultUserAvatarAsUrlFirebaseSourceHandler>();
    }
}
