using FuDever.Domain.Specifications.Base;
using FuDever.Domain.Specifications.Entities.Role;
using FuDever.PostgresSql.Commons;

namespace FuDever.PostgresSql.Specifications.Entities.Role;

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
            role.RemovedBy != Application.Commons.CommonConstant.App.DEFAULT_ENTITY_ID_AS_GUID &&
            role.RemovedAt != minDateTimeInDatabase;
    }
}
