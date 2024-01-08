using FuDeverWebApi.DataAccess.Data.Entities;
using FuDeverWebApi.DataAccess.Specifications.Generic.Implementations;
using System;

namespace FuDeverWebApi.DataAccess.Specifications.Entites.User;

public sealed class UserNotByIdSpecification :
    GenericSpecification<AppUserEntity>
{
    public UserNotByIdSpecification(Guid userId)
    {
        Criteria = user => user.Id != userId;
    }
}
