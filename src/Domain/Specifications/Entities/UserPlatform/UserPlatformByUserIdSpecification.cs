using FuDeverWebApi.DataAccess.Data.Entities;
using FuDeverWebApi.DataAccess.Specifications.Generic.Implementations;
using System;

namespace FuDeverWebApi.DataAccess.Specifications.Entites.UserPlatform;

public sealed class UserPlatformByUserIdSpecification :
    GenericSpecification<UserPlatformEntity>
{
    public UserPlatformByUserIdSpecification(Guid userId)
    {
        Criteria = userPlatform => userPlatform.UserId == userId;
    }
}
