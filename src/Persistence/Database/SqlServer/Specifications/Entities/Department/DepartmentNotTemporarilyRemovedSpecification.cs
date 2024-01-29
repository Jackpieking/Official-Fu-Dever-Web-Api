using Domain.Specifications.Base;
using Domain.Specifications.Entities.Department;
using Persistence.Commons;

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
            department.RemovedBy == Application.Commons.CommonConstant.App.DEFAULT_ENTITY_ID_AS_GUID &&
            department.RemovedAt == minDateTimeInDatabase;
    }
}
