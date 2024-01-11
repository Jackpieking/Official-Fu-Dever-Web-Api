using System;
using Domain.Specifications.Base;
using Domain.Specifications.Entities.User;
using Persistence.SqlServer2016.Common;

namespace Persistence.SqlServer2016.Specifications.Entities.User;

/// <summary>
///     Represent implementation of user not temporarily
///     removed specification.
/// </summary>
internal sealed class UserNotTemporarilyRemovedSpecification :
    BaseSpecification<Domain.Entities.User>,
    IUserNotTemporarilyRemovedSpecification
{
    internal UserNotTemporarilyRemovedSpecification()
    {
        var minDateTimeInDatabase = CustomConstant.DbDefaultValue.MIN_DATE_TIME;

        WhereExpression = user =>
            user.RemovedBy == Guid.Empty &&
            user.RemovedAt == minDateTimeInDatabase;
    }
}
