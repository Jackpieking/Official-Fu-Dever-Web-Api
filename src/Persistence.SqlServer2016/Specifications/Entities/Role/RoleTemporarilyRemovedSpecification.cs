using Domain.Specifications.Base;
using Domain.Specifications.Entities.Role;
using Persistence.SqlServer2016.Common;
using System;

namespace Persistence.SqlServer2016.Specifications.Entities.Role;

/// <summary>
///     Represent implementation of role temporarily
///     removed specification.
/// </summary>
internal sealed class RoleTemporarilyRemovedSpecification :
    BaseSpecification<Domain.Entities.Role>,
    IRoleTemporarilyRemovedSpecification
{
    internal RoleTemporarilyRemovedSpecification()
    {
        var minDateTimeInDatabase = CommonConstant.DbDefaultValue.MIN_DATE_TIME;

        WhereExpression = role =>
            role.RemovedBy != Guid.Empty &&
            role.RemovedAt != minDateTimeInDatabase;
    }
}
