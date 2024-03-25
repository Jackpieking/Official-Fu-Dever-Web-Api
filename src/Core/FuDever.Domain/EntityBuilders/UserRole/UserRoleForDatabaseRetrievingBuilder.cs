using FuDever.Domain.EntityBuilders.UserRole.Others;
using System;

namespace FuDever.Domain.EntityBuilders.UserRole;

/// <summary>
///     User role for database retrieving builder.
/// </summary>
public sealed class UserRoleForDatabaseRetrievingBuilder :
    Entities.UserRole,
    IBaseUserRoleBuilder,
    IUserRoleBuilder<UserRoleForDatabaseRetrievingBuilder>
{
    public Entities.UserRole Complete()
    {
        return new()
        {
            RoleId = RoleId,
            UserId = UserId
        };
    }

    public UserRoleForDatabaseRetrievingBuilder WithRoleId(Guid roleId)
    {
        RoleId = roleId;

        return this;
    }

    public UserRoleForDatabaseRetrievingBuilder WithUserId(Guid userId)
    {
        UserId = userId;

        return this;
    }
}
