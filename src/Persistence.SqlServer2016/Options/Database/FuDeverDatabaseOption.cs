namespace Persistence.SqlServer2016.Options.Database;

/// <summary>
///     Represent "FuDever" section in
///     appsetting.json configuration file.
/// </summary>
internal sealed class FuDeverDatabaseOption
{
    /// <summary>
    ///     Database connection string.
    /// </summary>
    internal string ConnectionString { get; set; }

    /// <summary>
    ///     Database command time out.
    /// </summary>
    internal int CommandTimeOut { get; set; }

    /// <summary>
    ///     Database number of retry on failure.
    /// </summary>
    internal int EnableRetryOnFailure { get; set; }

    /// <summary>
    ///     Name of assembly that contains migration.
    /// </summary>
    //internal string MigrationsAssembly { get; set; }

    /// <summary>
    ///     Database sensitive data logging.
    /// </summary>
    internal bool EnableSensitiveDataLogging { get; set; }

    /// <summary>
    ///     Database detail errors logging.
    /// </summary>
    internal bool EnableDetailedErrors { get; set; }

    /// <summary>
    ///     Database thread safety check.
    /// </summary>
    internal bool EnableThreadSafetyChecks { get; set; }

    /// <summary>
    ///     Database service provider caching.
    /// </summary>
    internal bool EnableServiceProviderCaching { get; set; }
}
