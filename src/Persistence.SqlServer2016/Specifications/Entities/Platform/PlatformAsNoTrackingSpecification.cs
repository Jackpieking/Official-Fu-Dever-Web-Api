using Domain.Specifications.Base;
using Domain.Specifications.Entities.Platform;

namespace Persistence.SqlServer2016.Specifications.Entities.Platform;

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
