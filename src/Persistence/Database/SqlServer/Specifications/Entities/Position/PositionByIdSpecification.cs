using Domain.Specifications.Base;
using Domain.Specifications.Entities.Position;
using System;

namespace Persistence.Database.SqlServer.Specifications.Entities.Position;

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
