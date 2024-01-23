using Domain.Specifications.Base;
using Domain.Specifications.Entities.Position;
using Persistence.Commons;
using System;

namespace Persistence.Database.SqlServer.Specifications.Entities.Position;

/// <summary>
///     Represent implementation of position not temporarily removed specification.
/// </summary>
internal sealed class PositionNotTemporarilyRemovedSpecification :
    BaseSpecification<Domain.Entities.Position>,
    IPositionNotTemporarilyRemovedSpecification
{
    internal PositionNotTemporarilyRemovedSpecification()
    {
        var minDateTimeInDatabase = CommonConstant.DbDefaultValue.MIN_DATE_TIME;

        WhereExpression = position =>
            position.RemovedBy == Guid.Empty &&
            position.RemovedAt == minDateTimeInDatabase;
    }
}
