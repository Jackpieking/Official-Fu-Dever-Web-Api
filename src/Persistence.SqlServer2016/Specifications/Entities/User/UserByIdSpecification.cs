using System;
using Domain.Specifications.Base;
using Domain.Specifications.Entities.User;

namespace Persistence.SqlServer2016.Specifications.Entities.User;

/// <summary>
///     Represent implementation of user by user id
///     specification.
/// </summary>
internal sealed class UserByIdSpecification :
    BaseSpecification<Domain.Entities.User>,
    IUserByIdSpecification
{
    internal UserByIdSpecification(Guid userId)
    {
        WhereExpression = user => user.Id == userId;
    }
}
