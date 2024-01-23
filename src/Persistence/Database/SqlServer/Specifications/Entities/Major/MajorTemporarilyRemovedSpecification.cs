using Domain.Specifications.Base;
using Domain.Specifications.Entities.Major;
using Persistence.Commons;
using System;

namespace Persistence.Database.SqlServer.Specifications.Entities.Major;

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
            major.RemovedBy != Guid.Empty &&
            major.RemovedAt != minDateTimeInDatabase;
    }
}


