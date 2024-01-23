using Domain.Specifications.Base;
using Domain.Specifications.Entities.User;
using System;

namespace Persistence.Database.SqlServer.Specifications.Entities.User;

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
