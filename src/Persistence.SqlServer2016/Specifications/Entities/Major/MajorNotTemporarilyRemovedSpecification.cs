using System;
using Domain.Specifications.Base;
using Domain.Specifications.Entities.Major;
using Persistence.SqlServer2016.Common;

namespace Persistence.SqlServer2016.Specifications.Entities.Major;

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
            major.RemovedBy == Guid.Empty &&
            major.RemovedAt == minDateTimeInDatabase;
    }
}
