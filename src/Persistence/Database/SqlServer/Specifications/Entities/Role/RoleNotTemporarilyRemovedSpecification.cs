using Domain.Specifications.Base;
using Domain.Specifications.Entities.Role;
using Persistence.Commons;

namespace Persistence.Database.SqlServer.Specifications.Entities.Role;

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
        var minDateTimeInDatabase = CommonConstant.DbDefaultValue.MIN_DATE_TIME;

        WhereExpression = role =>
            role.RemovedBy == Application.Commons.CommonConstant.App.DEFAULT_ENTITY_ID_AS_GUID &&
            role.RemovedAt == minDateTimeInDatabase;
    }
}
