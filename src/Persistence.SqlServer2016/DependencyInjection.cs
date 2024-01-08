using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Persistence.SqlServer2016.Data;
using Persistence.SqlServer2016.Options;

namespace Persistence.SqlServer2016;

public static class DependencyInjection
{
    public static void AddPersistenceSqlServer2016(
        this IServiceCollection services,
        IConfigurationManager configuration)
    {
        services.AddDbContextPool(configuration: configuration);
    }

    public static void AddDbContextPool(
        this IServiceCollection services,
        IConfigurationManager configuration)
    {
        services.AddDbContextPool<FuDeverContext>(optionsAction: config =>
        {
            var databaseOptions = configuration
                .GetRequiredSection(key: "Database")
                .GetRequiredSection(key: "FuDeverDb")
                .Get<DatabaseOption>();

            config.UseSqlServer(
                    connectionString: databaseOptions.ConnectionString,
                    sqlServerOptionsAction: databaseOptionsAction =>
                {
                    databaseOptionsAction
                        .CommandTimeout(commandTimeout: databaseOptions.CommandTimeOut)
                        .EnableRetryOnFailure(maxRetryCount: databaseOptions.EnableRetryOnFailure);
                })
                .EnableSensitiveDataLogging(sensitiveDataLoggingEnabled: databaseOptions.EnableSensitiveDataLogging)
                .EnableDetailedErrors(detailedErrorsEnabled: databaseOptions.EnableDetailedErrors)
                .EnableThreadSafetyChecks(enableChecks: databaseOptions.EnableThreadSafetyChecks)
                .EnableServiceProviderCaching(cacheServiceProvider: databaseOptions.EnableServiceProviderCaching);
        });
    }
}
