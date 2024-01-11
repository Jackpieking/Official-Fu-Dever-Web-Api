using Domain.Specifications.Base;
using Domain.Specifications.Entities.Position;

namespace Persistence.SqlServer2016.Specifications.Entities.Position;

/// <summary>
///     Represent implementation of position as no tracking specification.
/// </summary>
internal sealed class PositionAsNoTrackingSpecification :
    BaseSpecification<Domain.Entities.Position>,
    IPositionAsNoTrackingSpecification
{
    internal PositionAsNoTrackingSpecification()
    {
        IsAsNoTracking = true;
    }
}