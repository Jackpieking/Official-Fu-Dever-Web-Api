using Application.Interfaces.ExternalFiles;
using ExternalFile.Image.Firebase;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace ExternalFile;

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
    public static void AddImage(
        this IServiceCollection services,
        IConfigurationManager configuration)
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
