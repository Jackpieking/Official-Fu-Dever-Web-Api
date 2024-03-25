using FuDever.Domain.Specifications.Base;
using FuDever.Domain.Specifications.Entities.Role;

namespace FuDever.SqlServer.Specifications.Entities.Role;

/// <summary>
///     Represent implementation of role name is
///     not default specification.
/// </summary>
internal sealed class RoleNameIsNotDefaultSpecification :
    BaseSpecification<Domain.Entities.Role>,
    IRoleNameIsNotDefaultSpecification
{
    public RoleNameIsNotDefaultSpecification()
    {
        WhereExpression = role => !role.Name.Equals(string.Empty);
    }
}
