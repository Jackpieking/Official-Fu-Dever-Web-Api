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
    public string ConnectionString { get; set; }

    /// <summary>
    ///     Database command time out.
    /// </summary>
    public int CommandTimeOut { get; set; }

    /// <summary>
    ///     Database number of retry on failure.
    /// </summary>
    public int EnableRetryOnFailure { get; set; }

    /// <summary>
    ///     Database sensitive data logging.
    /// </summary>
    public bool EnableSensitiveDataLogging { get; set; }

    /// <summary>
    ///     Database detail errors logging.
    /// </summary>
    public bool EnableDetailedErrors { get; set; }

    /// <summary>
    ///     Database thread safety check.
    /// </summary>
    public bool EnableThreadSafetyChecks { get; set; }

    /// <summary>
    ///     Database service provider caching.
    /// </summary>
    public bool EnableServiceProviderCaching { get; set; }
}
