using System;
using Domain.Specifications.Base;
using Domain.Specifications.Entities.UserPlatform;

namespace Persistence.SqlServer2016.Specifications.Entities.UserPlatform;

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
