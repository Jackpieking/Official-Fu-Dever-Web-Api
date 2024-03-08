using Domain.Specifications.Base;
using Domain.Specifications.Entities.Major;
using Persistence.RelationalDatabase.SqlServer.Commons;

namespace Persistence.RelationalDatabase.SqlServer.Specifications.Entities.Major;

/// <summary>
///     Represent implementation of major temporarily removed specification.
/// </summary>
internal sealed class MajorTemporarilyRemovedSpecification :
    BaseSpecification<Domain.Entities.Major>,
    IMajorTemporarilyRemovedSpecification
{
    internal MajorTemporarilyRemovedSpecification()
    {
        var minDateTimeInDatabase = CommonConstant.DbDefaultValue.MIN_DATE_TIME;

        WhereExpression = major =>
            major.RemovedBy != Application.Commons.CommonConstant.App.DEFAULT_ENTITY_ID_AS_GUID &&
            major.RemovedAt != minDateTimeInDatabase;
    }
}


