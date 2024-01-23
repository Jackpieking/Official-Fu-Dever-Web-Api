using Domain.Specifications.Base;
using Domain.Specifications.Entities.Hobby;
using Persistence.Commons;
using System;

namespace Persistence.Database.SqlServer.Specifications.Entities.Hobby;

/// <summary>
///     Represent implementation of hobby not temporarily removed specification.
/// </summary>
internal sealed class HobbyNotTemporarilyRemovedSpecification :
    BaseSpecification<Domain.Entities.Hobby>,
    IHobbyNotTemporarilyRemovedSpecification
{
    internal HobbyNotTemporarilyRemovedSpecification()
    {
        var minDateTimeInDatabase = CommonConstant.DbDefaultValue.MIN_DATE_TIME;

        WhereExpression = hobby =>
            hobby.RemovedBy == Guid.Empty &&
            hobby.RemovedAt == minDateTimeInDatabase;
    }
}

