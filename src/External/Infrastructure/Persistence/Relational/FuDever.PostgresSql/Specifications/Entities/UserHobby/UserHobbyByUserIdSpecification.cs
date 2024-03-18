using FuDever.Domain.Specifications.Base;
using FuDever.Domain.Specifications.Entities.UserHobby;
using System;

namespace FuDever.PostgresSql.Specifications.Entities.UserHobby;

/// <summary>
///     Represent implementation of user hobby by
///     user id specification.
/// </summary>
internal sealed class UserHobbyByUserIdSpecification :
    BaseSpecification<Domain.Entities.UserHobby>,
    IUserHobbyByUserIdSpecification
{
    internal UserHobbyByUserIdSpecification(Guid userId)
    {
        WhereExpression = userHobby => userHobby.UserId == userId;
    }
}
