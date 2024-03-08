using Domain.Specifications.Base;
using Domain.Specifications.Entities.User;
using System;

namespace Persistence.RelationalDatabase.SqlServer.Specifications.Entities.User;

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
