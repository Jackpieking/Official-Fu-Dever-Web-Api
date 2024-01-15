using System;
using Domain.Specifications.Base;
using Domain.Specifications.Entities.User;
using Persistence.SqlServer2016.Common;

namespace Persistence.SqlServer2016.Specifications.Entities.User;

/// <summary>
///     Represent implementation of user temporarily
///     removed specification.
/// </summary>
internal sealed class UserTemporarilyRemovedSpecification :
    BaseSpecification<Domain.Entities.User>,
    IUserTemporarilyRemovedSpecification
{
    internal UserTemporarilyRemovedSpecification()
    {
        var minDateTimeInDatabase = CommonConstant.DbDefaultValue.MIN_DATE_TIME;

        WhereExpression = user =>
            user.RemovedBy != Guid.Empty &&
            user.RemovedAt != minDateTimeInDatabase;
    }
}
