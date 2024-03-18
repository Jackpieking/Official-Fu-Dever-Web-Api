using FuDever.Domain.Specifications.Base;
using FuDever.Domain.Specifications.Entities.Role;
using FuDever.PostgresSql.Commons;
using Microsoft.EntityFrameworkCore;

/// <summary>
///     Represent implementation of role by role
///     name specification.
/// </summary>
namespace FuDever.PostgresSql.Specifications.Entities.Role;

internal sealed class RoleByNameSpecification :
    BaseSpecification<Domain.Entities.Role>,
    IRoleByNameSpecification
{
    internal RoleByNameSpecification(
        string roleName,
        bool isCaseSensitive)
    {
        if (isCaseSensitive)
        {
            WhereExpression = role => role.Name.Equals(roleName);

            return;
        }

        WhereExpression = role => EF.Functions
            .Collate(
                role.Name,
                CommonConstant.DbCollation.CASE_INSENSITIVE)
            .Equals(roleName);
    }
}
