using System;
using Domain.Specifications.Base;
using Domain.Specifications.Entities.User;

namespace Persistence.SqlServer2016.Specifications.Entities.User;

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