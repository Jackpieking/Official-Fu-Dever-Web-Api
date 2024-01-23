using Domain.Specifications.Base;
using Domain.Specifications.Entities.Role;
using Microsoft.EntityFrameworkCore;
using Persistence.Commons;

/// <summary>
///     Represent implementation of role by role
///     name specification.
/// </summary>
namespace Persistence.Database.SqlServer.Specifications.Entities.Role;

internal sealed class RoleByNameSpecification :
    BaseSpecification<Domain.Entities.Role>,
    IRoleByNameSpecification
{
    internal RoleByNameSpecification(
        string roleName,
        bool isCaseSensitive)
    {
        if (!isCaseSensitive)
        {
            WhereExpression = role => role.Name.Equals(roleName);

            return;
        }

        WhereExpression = role => EF.Functions
            .Collate(
                role.Name,
                CommonConstant.DbCollation.SQL_LATIN1_GENERAL_CP1_CS_AS)
            .Equals(roleName);
    }
}
