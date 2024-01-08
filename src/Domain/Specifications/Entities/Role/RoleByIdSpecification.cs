using FuDeverWebApi.DataAccess.Data.Entities;
using FuDeverWebApi.DataAccess.Specifications.Generic.Implementations;
using System;

namespace FuDeverWebApi.DataAccess.Specifications.Entites.Role;

public sealed class RoleByIdSpecification :
    GenericSpecification<AppRoleEntity>
{
    public RoleByIdSpecification(Guid roleId)
    {
        Criteria = role => role.Id == roleId;
    }
}
