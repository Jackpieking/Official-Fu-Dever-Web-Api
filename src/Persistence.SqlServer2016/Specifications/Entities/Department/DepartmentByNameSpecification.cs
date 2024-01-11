using Domain.Specifications.Base;
using Domain.Specifications.Entities.Department;
using Microsoft.EntityFrameworkCore;

namespace Persistence.SqlServer2016.Specifications.Entities.Department;

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
        if (!isCaseSensitive)
        {
            WhereExpression = department => department.Name.Equals(departmentName);

            return;
        }

        WhereExpression = department => EF.Functions
            .Collate(
                department.Name,
                Common.CustomConstant.DbCollation.SQL_LATIN1_GENERAL_CP1_CS_AS)
            .Equals(departmentName);
    }
}

