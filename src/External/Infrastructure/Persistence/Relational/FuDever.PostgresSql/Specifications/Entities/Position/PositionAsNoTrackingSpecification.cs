using FuDever.Domain.Specifications.Base;
using FuDever.Domain.Specifications.Entities.Position;

namespace FuDever.PostgresSql.Specifications.Entities.Position;

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