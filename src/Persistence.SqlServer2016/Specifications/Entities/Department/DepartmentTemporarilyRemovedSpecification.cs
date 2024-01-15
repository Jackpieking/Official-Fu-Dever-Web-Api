using System;
using Domain.Specifications.Base;
using Domain.Specifications.Entities.Department;
using Persistence.SqlServer2016.Common;

namespace Persistence.SqlServer2016.Specifications.Entities.Department;

/// <summary>
///     Represent implementation of department temporarily removed specification.
/// </summary>
internal sealed class DepartmentTemporarilyRemovedSpecification :
    BaseSpecification<Domain.Entities.Department>,
    IDepartmentTemporarilyRemovedSpecification
{
    internal DepartmentTemporarilyRemovedSpecification()
    {
        var minDateTimeInDatabase = CommonConstant.DbDefaultValue.MIN_DATE_TIME;

        WhereExpression = department =>
            department.RemovedBy != Guid.Empty &&
            department.RemovedAt != minDateTimeInDatabase;
    }
}
