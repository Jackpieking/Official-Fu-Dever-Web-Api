using FuDeverWebApi.DataAccess.Data.Entities;
using FuDeverWebApi.DataAccess.Specifications.Generic.Implementations;
using System;

namespace FuDeverWebApi.DataAccess.Specifications.Entites.UserHobby;

public sealed class UserHobbyByUserIdSpecification :
    GenericSpecification<UserHobbyEntity>
{
    public UserHobbyByUserIdSpecification(Guid userId)
    {
        Criteria = userHobby => userHobby.UserId == userId;
    }
}
