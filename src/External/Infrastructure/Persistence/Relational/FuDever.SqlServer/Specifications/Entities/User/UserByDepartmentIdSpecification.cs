using FuDever.Domain.Specifications.Base;
using FuDever.Domain.Specifications.Entities.User;
using System;

namespace FuDever.SqlServer.Specifications.Entities.User;

/// <summary>
///     Represent implementation of user by department
///     id specification.
/// </summary>
internal sealed class UserByDepartmentIdSpecification :
    BaseSpecification<Domain.Entities.User>,
    IUserByDepartmentIdSpecification
{
    internal UserByDepartmentIdSpecification(Guid departmentId)
    {
        WhereExpression = user => user.DepartmentId == departmentId;
    }
}
