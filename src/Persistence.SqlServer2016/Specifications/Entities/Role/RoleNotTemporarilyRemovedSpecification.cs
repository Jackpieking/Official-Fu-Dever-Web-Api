using System;
using Domain.Specifications.Base;
using Domain.Specifications.Entities.Role;
using Persistence.SqlServer2016.Common;

namespace Persistence.SqlServer2016.Specifications.Entities.Role;

/// <summary>
///     Represent implementation of role not temporarily
///     removed specification.
/// </summary>
internal sealed class RoleNotTemporarilyRemovedSpecification :
    BaseSpecification<Domain.Entities.Role>,
    IRoleNotTemporarilyRemovedSpecification
{
    internal RoleNotTemporarilyRemovedSpecification()
    {
        var minDateTimeInDatabase = CustomConstant.DbDefaultValue.MIN_DATE_TIME;

        WhereExpression = role =>
            role.RemovedBy == Guid.Empty &&
            role.RemovedAt == minDateTimeInDatabase;
    }
}
