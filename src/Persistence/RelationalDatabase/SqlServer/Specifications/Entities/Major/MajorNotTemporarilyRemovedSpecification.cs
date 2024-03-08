using Domain.Specifications.Base;
using Domain.Specifications.Entities.Major;
using Persistence.RelationalDatabase.SqlServer.Commons;

namespace Persistence.RelationalDatabase.SqlServer.Specifications.Entities.Major;

/// <summary>
///     Represent implementation of major not temporarily removed specification.
/// </summary>
internal sealed class MajorNotTemporarilyRemovedSpecification :
    BaseSpecification<Domain.Entities.Major>,
    IMajorNotTemporarilyRemovedSpecification
{
    internal MajorNotTemporarilyRemovedSpecification()
    {
        var minDateTimeInDatabase = CommonConstant.DbDefaultValue.MIN_DATE_TIME;

        WhereExpression = major =>
            major.RemovedBy == Application.Commons.CommonConstant.App.DEFAULT_ENTITY_ID_AS_GUID &&
            major.RemovedAt == minDateTimeInDatabase;
    }
}
