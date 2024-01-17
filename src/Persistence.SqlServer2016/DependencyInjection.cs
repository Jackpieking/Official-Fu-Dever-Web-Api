﻿using System;
using System.Reflection;
using Domain.Entities;
using Domain.Specifications.Others.Interfaces;
using Domain.UnitOfWorks;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Persistence.SqlServer2016.Data;
using Persistence.SqlServer2016.Options.AspNetCoreIdentity;
using Persistence.SqlServer2016.Options.Database;
using Persistence.SqlServer2016.Specifications.Others;
using Persistence.SqlServer2016.UnitOfWorks;

namespace Persistence.SqlServer2016;

/// <summary>
///     Config services for this layer.
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
    public static void AddPersistenceSqlServer2016(
        this IServiceCollection services,
        IConfigurationManager configuration)
    {
        services.ConfigureDbContextPool(configuration: configuration);

        services.ConfigureCore();

        services.ConfigureAspNetCoreIdentity(configuration: configuration);
    }

    /// <summary>
    ///     Config the db context pool service.
    /// </summary>
    /// <param name="services">
    ///     Service container.
    /// </param>
    /// <param name="configuration">
    ///     Load configuration for configuration
    ///     file (appsetting).
    /// </param>
    private static void ConfigureDbContextPool(
        this IServiceCollection services,
        IConfigurationManager configuration)
    {
        services.AddDbContextPool<FuDeverContext>(optionsAction: (provider, config) =>
        {
            const string DatabaseSection = "Database";
            const string FuDeverDbSection = "FuDever";

            var fuDeverDatabaseOption = configuration
                .GetRequiredSection(key: DatabaseSection)
                .GetRequiredSection(key: FuDeverDbSection)
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
    ///     Config core services.
    /// </summary>
    /// <param name="services">
    ///     Service container.
    /// </param>
    private static void ConfigureCore(this IServiceCollection services)
    {
        services
            .AddScoped<IUnitOfWork, UnitOfWork>()
            .AddScoped<ISuperSpecificationManager, SuperSpecificationManager>();
    }

    /// <summary>
    ///     Config asp net core identity service.
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
                const string AspNetCoreIdentitySection = "AspNetCoreIdentity";

                var aspNetCoreIdentityOption = configuration
                    .GetRequiredSection(key: AspNetCoreIdentitySection)
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
