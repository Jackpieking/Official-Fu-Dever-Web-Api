using FuDeverWebApi.DataAccess.Data.Entities;
using FuDeverWebApi.DataAccess.Specifications.Generic.Implementations;
using FuDeverWebApi.Helpers;
using Microsoft.EntityFrameworkCore;

namespace FuDeverWebApi.DataAccess.Specifications.Entites.Role;

public sealed class RoleByNameSpecification :
    GenericSpecification<AppRoleEntity>
{
    public RoleByNameSpecification(
        string roleName,
        bool isCaseSensitive = false)
    {
        if (!isCaseSensitive)
        {
            Criteria = role => role.Name.Equals(roleName);

            return;
        }

        Criteria = role => EF.Functions
            .Collate(
                role.Name,
                CustomConstants.SqlCollation.SQL_LATIN1_GENERAL_CP1_CS_AS)
            .Equals(roleName);
    }
}
