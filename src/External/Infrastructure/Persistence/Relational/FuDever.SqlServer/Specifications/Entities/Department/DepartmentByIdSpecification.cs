using FuDever.Domain.Specifications.Base;
using FuDever.Domain.Specifications.Entities.Department;
using System;

namespace FuDever.SqlServer.Specifications.Entities.Department;

/// <summary>
///     Represent implementation of department by department id specification.
/// </summary>
internal sealed class DepartmentByIdSpecification :
    BaseSpecification<Domain.Entities.Department>,
    IDepartmentByIdSpecification
{
    internal DepartmentByIdSpecification(Guid departmentId)
    {
        WhereExpression = department => department.Id == departmentId;
    }
}
