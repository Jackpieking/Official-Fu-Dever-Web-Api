using Domain.Specifications.Base;
using Domain.Specifications.Entities.Position;
using Persistence.Commons;

namespace Persistence.Database.SqlServer.Specifications.Entities.Position;

/// <summary>
///     Represent implementation of platform temporarily removed specification.
/// </summary>
internal sealed class PositionTemporarilyRemovedSpecification :
    BaseSpecification<Domain.Entities.Position>,
    IPositionTemporarilyRemovedSpecification
{
    internal PositionTemporarilyRemovedSpecification()
    {
        var minDateTimeInDatabase = CommonConstant.DbDefaultValue.MIN_DATE_TIME;

        WhereExpression = position =>
            position.RemovedBy != Application.Commons.CommonConstant.App.DEFAULT_ENTITY_ID_AS_GUID &&
            position.RemovedAt != minDateTimeInDatabase;
    }
}
