using System;
using Domain.Specifications.Base;
using Domain.Specifications.Entities.User;

namespace Persistence.SqlServer2016.Specifications.Entities.User;

/// <summary>
///     Represent implementation of user not by id specification.
/// </summary>
internal sealed class UserNotByIdSpecification :
    BaseSpecification<Domain.Entities.User>,
    IUserNotByIdSpecification
{
    internal UserNotByIdSpecification(Guid userId)
    {
        WhereExpression = user => user.Id != userId;
    }
}
