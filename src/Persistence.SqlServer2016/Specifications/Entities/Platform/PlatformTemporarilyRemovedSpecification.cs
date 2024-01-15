using System;
using Domain.Specifications.Base;
using Domain.Specifications.Entities.Platform;
using Persistence.SqlServer2016.Common;

namespace Persistence.SqlServer2016.Specifications.Entities.Platform;

/// <summary>
///     Represent implementation of platform temporarily removed specification.
/// </summary>
internal sealed class PlatformTemporarilyRemovedSpecification :
    BaseSpecification<Domain.Entities.Platform>,
    IPlatformTemporarilyRemovedSpecification
{
    internal PlatformTemporarilyRemovedSpecification()
    {
        var minDateTimeInDatabase = CommonConstant.DbDefaultValue.MIN_DATE_TIME;

        WhereExpression = platform =>
            platform.RemovedBy != Guid.Empty &&
            platform.RemovedAt != minDateTimeInDatabase;
    }
}
