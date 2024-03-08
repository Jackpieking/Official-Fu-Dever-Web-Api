using Domain.Specifications.Base;
using Domain.Specifications.Entities.Position;
using Persistence.RelationalDatabase.SqlServer.Commons;

namespace Persistence.RelationalDatabase.SqlServer.Specifications.Entities.Position;

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
            position.RemovedBy == Application.Commons.CommonConstant.App.DEFAULT_ENTITY_ID_AS_GUID &&
            position.RemovedAt == minDateTimeInDatabase;
    }
}
