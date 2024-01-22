using Domain.Specifications.Base;
using Domain.Specifications.Entities.Department;
using System;

namespace Persistence.SqlServer2016.Specifications.Entities.Department;

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
