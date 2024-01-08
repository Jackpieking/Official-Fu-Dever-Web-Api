using FuDeverWebApi.DataAccess.Data.Entities;
using FuDeverWebApi.DataAccess.Specifications.Generic.Implementations;
using System;

namespace FuDeverWebApi.DataAccess.Specifications.Entites.User;

public sealed class UserByIdSpecification :
    GenericSpecification<AppUserEntity>
{
    public UserByIdSpecification(Guid userId)
    {
        Criteria = user => user.Id == userId;
    }
}
