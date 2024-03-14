using FuDever.Domain.Specifications.Base;
using FuDever.Domain.Specifications.Entities.UserPlatform;
using System;

namespace FuDever.SqlServer.Specifications.Entities.UserPlatform;

/// <summary>
///     Represent implementation of user platform by user
///     id specification.
/// </summary>
internal sealed class UserPlatformByUserIdSpecification :
    BaseSpecification<Domain.Entities.UserPlatform>,
    IUserPlatformByUserIdSpecification
{
    internal UserPlatformByUserIdSpecification(Guid userId)
    {
        WhereExpression = userPlatform => userPlatform.UserId == userId;
    }
}
