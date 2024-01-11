using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;

namespace Persistence.SqlServer2016.Options.AspNetCoreIdentity;

/// <summary>
///     load the payload of "AspNetCoreIdentity" section in
///     appsetting.json configuration file to memory.
/// </summary>
internal sealed class AspNetCoreIdentityOptionSetup : IConfigureOptions<AspNetCoreIdentityOption>
{
    private readonly IConfiguration _configuration;

    internal AspNetCoreIdentityOptionSetup(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public void Configure(AspNetCoreIdentityOption options)
    {
        const string AspNetCoreIdentitySection = "AspNetCoreIdentity";

        _configuration
            .GetRequiredSection(key: AspNetCoreIdentitySection)
            .Bind(instance: options);
    }
}
