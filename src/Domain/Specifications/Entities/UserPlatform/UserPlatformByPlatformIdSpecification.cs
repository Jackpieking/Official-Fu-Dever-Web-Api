using FuDeverWebApi.DataAccess.Data.Entities;
using FuDeverWebApi.DataAccess.Specifications.Generic.Implementations;
using System;

namespace FuDeverWebApi.DataAccess.Specifications.Entites.UserPlatform;

public sealed class UserPlatformByPlatformIdSpecification :
    GenericSpecification<UserPlatformEntity>
{
    public UserPlatformByPlatformIdSpecification(Guid platformId)
    {
        Criteria = userPlatform => userPlatform.PlatformId == platformId;
    }
}
