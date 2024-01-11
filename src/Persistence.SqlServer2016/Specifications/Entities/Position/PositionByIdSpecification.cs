using System;
using Domain.Specifications.Base;
using Domain.Specifications.Entities.Position;

namespace Persistence.SqlServer2016.Specifications.Entities.Position;

/// <summary>
///     Represent implementation of position by position id specification.
/// </summary>
internal sealed class PositionByIdSpecification :
    BaseSpecification<Domain.Entities.Position>,
    IPositionByIdSpecification
{
    internal PositionByIdSpecification(Guid positionId)
    {
        WhereExpression = position => position.Id == positionId;
    }
}
