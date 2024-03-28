using FuDever.Application.Interfaces.Mail;
using FuDever.GoogleSmtp.Handler;
using Microsoft.Extensions.DependencyInjection;

namespace FuDever.GoogleSmtp;

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
    public static void AddNotification(this IServiceCollection services)
    {
        services.ConfigureMail();
    }

    /// <summary>
    ///     Configure mail services.
    /// </summary>
    /// <param name="services">
    ///     Service container.
    /// </param>
    private static void ConfigureMail(this IServiceCollection services)
    {
        services.AddSingleton<IMailHandler, GoogleMailHandler>();
    }
}
