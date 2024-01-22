using Domain.Specifications.Base;
using Domain.Specifications.Entities.Platform;
using Persistence.SqlServer2016.Common;
using System;

namespace Persistence.SqlServer2016.Specifications.Entities.Platform;

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
            platform.RemovedBy == Guid.Empty &&
            platform.RemovedAt == minDateTimeInDatabase;
    }
}