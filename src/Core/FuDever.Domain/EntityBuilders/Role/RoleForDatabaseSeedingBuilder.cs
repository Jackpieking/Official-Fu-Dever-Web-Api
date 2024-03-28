using FuDever.Domain.EntityBuilders.Role.Others;
using System;

namespace FuDever.Domain.EntityBuilders.Role;

/// <summary>
///     Role for database seeding builder.
/// </summary>
public sealed class RoleForDatabaseSeedingBuilder :
    Entities.Role,
    IBaseRoleBuilder,
    IRoleBuilder<RoleForDatabaseSeedingBuilder>
{
    public void Clear()
    {
        Id = Guid.Empty;
        Name = default;
        NormalizedName = default;
        ConcurrencyStamp = default;
        RemovedAt = default;
        RemovedBy = Guid.Empty;
    }

    public Entities.Role Complete()
    {
        return new()
        {
            Id = Id,
            Name = Name,
            NormalizedName = NormalizedName,
            ConcurrencyStamp = ConcurrencyStamp,
            RemovedAt = RemovedAt,
            RemovedBy = RemovedBy
        };
    }

    public RoleForDatabaseSeedingBuilder WithId(Guid roleId)
    {
        // Validate role Id.
        if (roleId == Guid.Empty)
        {
            return default;
        }

        Id = roleId;

        return this;
    }

    public RoleForDatabaseSeedingBuilder WithName(string roleName)
    {
        Name = roleName;

        return this;
    }

    public RoleForDatabaseSeedingBuilder WithNormalizedName(string roleNormalizedName)
    {
        NormalizedName = roleNormalizedName;

        return this;
    }

    public RoleForDatabaseSeedingBuilder WithConcurrencyStamp(string roleConcurrencyStamp)
    {
        ConcurrencyStamp = roleConcurrencyStamp;

        return this;
    }

    public RoleForDatabaseSeedingBuilder WithRemovedAt(DateTime roleRemovedAt)
    {
        // Validate role removed at.
        if (roleRemovedAt == DateTime.MaxValue ||
            roleRemovedAt.Kind != DateTimeKind.Utc)
        {
            return default;
        }

        RemovedAt = roleRemovedAt;

        return this;
    }

    public RoleForDatabaseSeedingBuilder WithRemovedBy(Guid roleRemovedBy)
    {
        // Validate role removed by.
        if (roleRemovedBy == Guid.Empty)
        {
            return default;
        }

        RemovedBy = roleRemovedBy;

        return this;
    }
}
