using System;
using System.Reflection;
using Domain.Data;
using Domain.Entities;
using Domain.Specifications.Others.Interfaces;
using Domain.UnitOfWorks;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Persistence.SqlServer2016.Data;
using Persistence.SqlServer2016.Options.AspNetCoreIdentity;
using Persistence.SqlServer2016.Options.Database;
using Persistence.SqlServer2016.Specifications.Others;
using Persistence.SqlServer2016.UnitOfWorks;

namespace Persistence.SqlServer2016;

/// <summary>
///
/// </summary>
public static class DependencyInjection
{
    /// <summary>
    ///
    /// </summary>
    /// <param name="services">
    /// </param>
    /// <param name="configuration">
    /// </param>
    public static void AddPersistenceSqlServer2016(
        this IServiceCollection services,
        IConfigurationManager configuration)
    {
        services.ConfigDbContextPool();

        services.ConfigCore();

        services.ConfigAspNetCoreIdentity();
    }

    /// <summary>
    ///
    /// </summary>
    /// <param name="services">
    /// </param>
    /// <param name="configuration">
    /// </param>
    public static void ConfigDbContextPool(this IServiceCollection services)
    {
        services.AddDbContextPool<FuDeverContext>(optionsAction: (provider, config) =>
        {
            var databaseOptions = provider.GetRequiredService<IOptions<FuDeverDatabaseOption>>().Value;

            config.UseSqlServer(
                    connectionString: databaseOptions.ConnectionString,
                    sqlServerOptionsAction: databaseOptionsAction =>
                {
                    databaseOptionsAction
                        .CommandTimeout(commandTimeout: databaseOptions.CommandTimeOut)
                        .EnableRetryOnFailure(maxRetryCount: databaseOptions.EnableRetryOnFailure)
                        .MigrationsAssembly(assemblyName: Assembly.GetExecutingAssembly().FullName);
                })
                .EnableSensitiveDataLogging(sensitiveDataLoggingEnabled: databaseOptions.EnableSensitiveDataLogging)
                .EnableDetailedErrors(detailedErrorsEnabled: databaseOptions.EnableDetailedErrors)
                .EnableThreadSafetyChecks(enableChecks: databaseOptions.EnableThreadSafetyChecks)
                .EnableServiceProviderCaching(cacheServiceProvider: databaseOptions.EnableServiceProviderCaching);
        });

        services.AddScoped<IFuDeverContext>(implementationFactory: provider =>
            provider.GetRequiredService<FuDeverContext>());
    }

    /// <summary>
    ///
    /// </summary>
    /// <param name="services">
    /// </param>
    public static void ConfigCore(this IServiceCollection services)
    {
        services
            .ConfigureOptions<FuDeverDatabaseOptionSetup>()
            .ConfigureOptions<AspNetCoreIdentityOptionSetup>();

        services
            .AddScoped<IUnitOfWork, UnitOfWork>()
            .AddScoped<ISuperSpecificationManager, SuperSpecificationManager>();
    }

    /// <summary>
    ///
    /// </summary>
    /// <param name="services">
    /// </param>
    public static void ConfigAspNetCoreIdentity(this IServiceCollection services)
    {
        services
            .AddIdentity<User, Role>(setupAction: options =>
            {
                options.Password.RequireDigit = true;
                options.Password.RequireLowercase = false;
                options.Password.RequireNonAlphanumeric = true;
                options.Password.RequireUppercase = true;
                options.Password.RequiredLength = 4;
                options.Password.RequiredUniqueChars = 0;

                options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(value: 1);
                options.Lockout.MaxFailedAccessAttempts = 3;
                options.Lockout.AllowedForNewUsers = true;

                options.User.AllowedUserNameCharacters = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789-._@+";
                options.User.RequireUniqueEmail = true;

                options.SignIn.RequireConfirmedEmail = false;
                options.SignIn.RequireConfirmedPhoneNumber = false;
            })
            .AddEntityFrameworkStores<FuDeverContext>()
            .AddDefaultTokenProviders();
    }
}
