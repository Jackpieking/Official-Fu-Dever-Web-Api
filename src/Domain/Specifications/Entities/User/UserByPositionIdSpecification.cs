using FuDeverWebApi.DataAccess.Data.Entities;
using FuDeverWebApi.DataAccess.Specifications.Generic.Implementations;
using System;

namespace FuDeverWebApi.DataAccess.Specifications.Entites.User;

public sealed class UserByPositionIdSpecification :
    GenericSpecification<AppUserEntity>
{
    public UserByPositionIdSpecification(Guid positionId)
    {
        Criteria = user => user.PositionId == positionId;
    }
}