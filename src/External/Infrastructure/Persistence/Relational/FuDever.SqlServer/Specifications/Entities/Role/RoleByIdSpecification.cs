using FuDever.Domain.Specifications.Base;
using FuDever.Domain.Specifications.Entities.Role;
using System;

namespace FuDever.SqlServer.Specifications.Entities.Role;

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
