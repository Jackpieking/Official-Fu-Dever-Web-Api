using FuDeverWebApi.DataAccess.Data.Entities;
using FuDeverWebApi.DataAccess.Specifications.Generic.Implementations;
using System;

namespace FuDeverWebApi.DataAccess.Specifications.Entites.User;

public sealed class UserByDepartmentIdSpecification :
    GenericSpecification<AppUserEntity>
{
    public UserByDepartmentIdSpecification(Guid departmentId)
    {
        Criteria = user => user.DepartmentId == departmentId;
    }
}
