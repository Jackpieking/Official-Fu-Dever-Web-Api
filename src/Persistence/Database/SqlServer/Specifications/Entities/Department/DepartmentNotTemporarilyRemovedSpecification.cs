using Domain.Specifications.Base;
using Domain.Specifications.Entities.Department;
using Persistence.Commons;
using System;

namespace Persistence.Database.SqlServer.Specifications.Entities.Department;

/// <summary>
///     Represent implementation of department not temporarily removed specification.
/// </summary>
internal sealed class DepartmentNotTemporarilyRemovedSpecification :
    BaseSpecification<Domain.Entities.Department>,
    IDepartmentNotTemporarilyRemovedSpecification
{
    internal DepartmentNotTemporarilyRemovedSpecification()
    {
        var minDateTimeInDatabase = CommonConstant.DbDefaultValue.MIN_DATE_TIME;

        WhereExpression = department =>
            department.RemovedBy == Guid.Empty &&
            department.RemovedAt == minDateTimeInDatabase;
    }
}
