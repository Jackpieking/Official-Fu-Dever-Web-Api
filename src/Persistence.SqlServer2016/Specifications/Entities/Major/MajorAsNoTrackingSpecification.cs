using Domain.Specifications.Base;
using Domain.Specifications.Entities.Major;

namespace Persistence.SqlServer2016.Specifications.Entities.Major;

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
