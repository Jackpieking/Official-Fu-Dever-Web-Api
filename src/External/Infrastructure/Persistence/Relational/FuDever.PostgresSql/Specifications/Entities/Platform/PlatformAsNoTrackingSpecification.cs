using FuDever.Domain.Specifications.Base;
using FuDever.Domain.Specifications.Entities.Platform;

namespace FuDever.PostgresSql.Specifications.Entities.Platform;

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
