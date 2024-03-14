using FuDever.Domain.Specifications.Base;
using FuDever.Domain.Specifications.Entities.Major;
using FuDever.SqlServer.Commons;

namespace FuDever.SqlServer.Specifications.Entities.Major;

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
