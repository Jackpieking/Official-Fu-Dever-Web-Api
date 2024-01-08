using FuDeverWebApi.DataAccess.Data.Entities;
using FuDeverWebApi.DataAccess.Specifications.Generic.Implementations;
using System;

namespace FuDeverWebApi.DataAccess.Specifications.Entites.User;

public sealed class UserByMajorIdSpecification :
    GenericSpecification<AppUserEntity>
{
    public UserByMajorIdSpecification(Guid majorId)
    {
        Criteria = user => user.MajorId == majorId;
    }
}

