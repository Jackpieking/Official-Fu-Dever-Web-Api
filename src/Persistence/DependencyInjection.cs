using Application.Interfaces.Data;
using Configuration.Infrastructure.Persistence.AspNetCoreIdentity;
using Configuration.Infrastructure.Persistence.Database;
using Domain.Entities;
using Domain.Specifications.Others.Interfaces;
using Domain.UnitOfWorks;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Persistence.Database.SqlServer.Data;
using Persistence.Database.SqlServer.Specifications.Others;
using Persistence.UnitOfWorks;
using System;
using System.Reflection;

namespace Persistence.SqlServer2016;

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
    public static void AddPersistence(
        this IServiceCollection services,
        IConfigurationManager configuration)
    {
        services.ConfigureSqlServerDbContextPool(configuration: configuration);

        services.ConfigureCore();

        services.ConfigureAspNetCoreIdentity(configuration: configuration);
    }

    /// <summary>
    ///     Configure the db context pool service.
    /// </summary>
    /// <param name="services">
    ///     Service container.
    /// </param>
    /// <param name="configuration">
    ///     Load configuration for configuration
    ///     file (appsetting).
    /// </param>
    private static void ConfigureSqlServerDbContextPool(
        this IServiceCollection services,
        IConfigurationManager configuration)
    {
        services.AddDbContextPool<FuDeverContext>(optionsAction: (provider, config) =>
        {
            var fuDeverDatabaseOption = configuration
                .GetRequiredSection(key: "Database")
                .GetRequiredSection(key: "FuDever")
                .Get<FuDeverDatabaseOption>();

            config.UseSqlServer(
                    connectionString: fuDeverDatabaseOption.ConnectionString,
                    sqlServerOptionsAction: databaseOptionsAction =>
                {
                    databaseOptionsAction
                        .CommandTimeout(commandTimeout: fuDeverDatabaseOption.CommandTimeOut)
                        .EnableRetryOnFailure(maxRetryCount: fuDeverDatabaseOption.EnableRetryOnFailure)
                        .MigrationsAssembly(assemblyName: Assembly.GetExecutingAssembly().GetName().Name);
                })
                .EnableSensitiveDataLogging(sensitiveDataLoggingEnabled: fuDeverDatabaseOption.EnableSensitiveDataLogging)
                .EnableDetailedErrors(detailedErrorsEnabled: fuDeverDatabaseOption.EnableDetailedErrors)
                .EnableThreadSafetyChecks(enableChecks: fuDeverDatabaseOption.EnableThreadSafetyChecks)
                .EnableServiceProviderCaching(cacheServiceProvider: fuDeverDatabaseOption.EnableServiceProviderCaching);
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
        services
            .AddScoped<IUnitOfWork, UnitOfWork>()
            .AddScoped<ISuperSpecificationManager, SuperSpecificationManager>()
            .AddScoped<IDbMinTimeHandler, DbMinTimeHandler>();
    }

    /// <summary>
    ///     Configure asp net core identity service.
    /// </summary>
    /// <param name="services">
    ///     Service container.
    /// </param>
    /// <param name="configuration">
    ///     Load configuration for configuration
    ///     file (appsetting).
    /// </param>
    private static void ConfigureAspNetCoreIdentity(
        this IServiceCollection services,
        IConfigurationManager configuration)
    {
        services
            .AddIdentity<User, Role>(setupAction: config =>
            {
                var aspNetCoreIdentityOption = configuration
                    .GetRequiredSection(key: "AspNetCoreIdentity")
                    .Get<AspNetCoreIdentityOption>();

                config.Password.RequireDigit = aspNetCoreIdentityOption.Password.RequireDigit;
                config.Password.RequireLowercase = aspNetCoreIdentityOption.Password.RequireLowercase;
                config.Password.RequireNonAlphanumeric = aspNetCoreIdentityOption.Password.RequireNonAlphanumeric;
                config.Password.RequireUppercase = aspNetCoreIdentityOption.Password.RequireUppercase;
                config.Password.RequiredLength = aspNetCoreIdentityOption.Password.RequiredLength;
                config.Password.RequiredUniqueChars = aspNetCoreIdentityOption.Password.RequiredUniqueChars;

                config.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromSeconds(value:
                    aspNetCoreIdentityOption.Lockout.DefaultLockoutTimeSpanInSecond);
                config.Lockout.MaxFailedAccessAttempts = aspNetCoreIdentityOption.Lockout.MaxFailedAccessAttempts;
                config.Lockout.AllowedForNewUsers = aspNetCoreIdentityOption.Lockout.AllowedForNewUsers;

                config.User.AllowedUserNameCharacters = aspNetCoreIdentityOption.User.AllowedUserNameCharacters;
                config.User.RequireUniqueEmail = aspNetCoreIdentityOption.User.RequireUniqueEmail;

                config.SignIn.RequireConfirmedEmail = aspNetCoreIdentityOption.SignIn.RequireConfirmedEmail;
                config.SignIn.RequireConfirmedPhoneNumber = aspNetCoreIdentityOption.SignIn.RequireConfirmedPhoneNumber;
            })
            .AddEntityFrameworkStores<FuDeverContext>()
            .AddDefaultTokenProviders();
    }
}
