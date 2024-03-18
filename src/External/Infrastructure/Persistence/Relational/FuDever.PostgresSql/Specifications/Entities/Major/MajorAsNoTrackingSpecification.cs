using FuDever.Domain.Specifications.Base;
using FuDever.Domain.Specifications.Entities.Major;

namespace FuDever.PostgresSql.Specifications.Entities.Major;

/// <summary>
///     Represent implementation of major as no tracking specification.
/// </summary>
internal sealed class MajorAsNoTrackingSpecification :
    BaseSpecification<Domain.Entities.Major>,
    IMajorAsNoTrackingSpecification
{
    internal MajorAsNoTrackingSpecification()
    {
        IsAsNoTracking = true;
    }
}
