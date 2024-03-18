using FuDever.Domain.Specifications.Base;
using FuDever.Domain.Specifications.Entities.Department;
using FuDever.PostgresSql.Commons;
using Microsoft.EntityFrameworkCore;

namespace FuDever.PostgresSql.Specifications.Entities.Department;

/// <summary>
///     Represent implementation of department by department name specification.
/// </summary>
internal sealed class DepartmentByNameSpecification :
    BaseSpecification<Domain.Entities.Department>,
    IDepartmentByNameSpecification
{
    internal DepartmentByNameSpecification(
        string departmentName,
        bool isCaseSensitive)
    {
        if (isCaseSensitive)
        {
            WhereExpression = department => department.Name.Equals(departmentName);

            return;
        }

        WhereExpression = department => EF.Functions
            .Collate(
                department.Name,
                CommonConstant.DbCollation.CASE_INSENSITIVE)
            .Equals(departmentName);
    }
}
