using FuDever.Domain.Specifications.Base;
using FuDever.Domain.Specifications.Entities.User;
using System;

namespace FuDever.PostgresSql.Specifications.Entities.User;

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
