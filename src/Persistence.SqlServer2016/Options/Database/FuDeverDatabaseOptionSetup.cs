using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;

namespace Persistence.SqlServer2016.Options.Database;

/// <summary>
///     load the payload of "FuDever" section in
///     appsetting.json configuration file to memory.
/// </summary>
internal sealed class FuDeverDatabaseOptionSetup : IConfigureOptions<FuDeverDatabaseOption>
{
    private readonly IConfiguration _configuration;

    internal FuDeverDatabaseOptionSetup(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public void Configure(FuDeverDatabaseOption options)
    {
        const string DatabaseSection = "Database";
        const string FuDeverDbSection = "FuDever";

        _configuration
            .GetRequiredSection(key: DatabaseSection)
            .GetRequiredSection(key: FuDeverDbSection)
            .Bind(instance: options);
    }
}
