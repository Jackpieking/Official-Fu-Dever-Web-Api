using FuDever.Domain.Specifications.Base;
using FuDever.Domain.Specifications.Entities.Major;
using FuDever.PostgresSql.Commons;

namespace FuDever.PostgresSql.Specifications.Entities.Major;

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
