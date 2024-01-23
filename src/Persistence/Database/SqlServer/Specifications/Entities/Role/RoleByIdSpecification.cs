using Domain.Specifications.Base;
using Domain.Specifications.Entities.Role;
using System;

namespace Persistence.Database.SqlServer.Specifications.Entities.Role;

/// <summary>
///     Represent implementation of role by role
///     id specification.
/// </summary>
internal sealed class RoleByIdSpecification :
    BaseSpecification<Domain.Entities.Role>,
    IRoleByIdSpecification
{
    internal RoleByIdSpecification(Guid roleId)
    {
        WhereExpression = role => role.Id == roleId;
    }
}
