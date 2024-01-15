using System;
using Domain.Specifications.Base;
using Domain.Specifications.Entities.Hobby;
using Persistence.SqlServer2016.Common;

namespace Persistence.SqlServer2016.Specifications.Entities.Hobby;

/// <summary>
///     Represent implementation of hobby temporarily removed specification.
/// </summary>
internal sealed class HobbyTemporarilyRemovedSpecification :
    BaseSpecification<Domain.Entities.Hobby>,
    IHobbyTemporarilyRemovedSpecification
{
    internal HobbyTemporarilyRemovedSpecification()
    {
        var minDateTimeInDatabase = CommonConstant.DbDefaultValue.MIN_DATE_TIME;

        WhereExpression = hobby =>
            hobby.RemovedBy != Guid.Empty &&
            hobby.RemovedAt != minDateTimeInDatabase;
    }
}

