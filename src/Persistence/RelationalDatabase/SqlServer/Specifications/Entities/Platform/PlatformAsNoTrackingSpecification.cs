using Domain.Specifications.Base;
using Domain.Specifications.Entities.Platform;

namespace Persistence.RelationalDatabase.SqlServer.Specifications.Entities.Platform;

/// <summary>
///     Represent implementation of platform as no tracking specification.
/// </summary>
internal sealed class PlatformAsNoTrackingSpecification :
    BaseSpecification<Domain.Entities.Platform>,
    IPlatformAsNoTrackingSpecification
{
    internal PlatformAsNoTrackingSpecification()
    {
        IsAsNoTracking = true;
    }
}
