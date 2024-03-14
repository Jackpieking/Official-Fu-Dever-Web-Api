using FuDever.Domain.Specifications.Base;
using FuDever.Domain.Specifications.Entities.Platform;
using FuDever.SqlServer.Commons;

namespace FuDever.SqlServer.Specifications.Entities.Platform;

/// <summary>
///     Represent implementation of platform not temporarily removed specification.
/// </summary>
internal sealed class PlatformNotTemporarilyRemovedSpecification :
    BaseSpecification<Domain.Entities.Platform>,
    IPlatformNotTemporarilyRemovedSpecification
{
    internal PlatformNotTemporarilyRemovedSpecification()
    {
        var minDateTimeInDatabase = CommonConstant.DbDefaultValue.MIN_DATE_TIME;

        WhereExpression = platform =>
            platform.RemovedBy == Application.Commons.CommonConstant.App.DEFAULT_ENTITY_ID_AS_GUID &&
            platform.RemovedAt == minDateTimeInDatabase;
    }
}