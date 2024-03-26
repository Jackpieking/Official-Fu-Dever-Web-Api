using FuDever.Domain.EntityBuilders.Role.Others;
using System;

namespace FuDever.Domain.EntityBuilders.Role;

/// <summary>
///     Role for database retrieving builder.
/// </summary>
public sealed class RoleForDatabaseRetrievingBuilder :
    Entities.Role,
    IBaseRoleBuilder,
    IRoleBuilder<RoleForDatabaseRetrievingBuilder>
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

    public RoleForDatabaseRetrievingBuilder WithId(Guid roleId)
    {
        Id = roleId;

        return this;
    }

    public RoleForDatabaseRetrievingBuilder WithName(string roleName)
    {
        Name = roleName;

        return this;
    }

    public RoleForDatabaseRetrievingBuilder WithNormalizedName(string roleNormalizedName)
    {
        NormalizedName = roleNormalizedName;

        return this;
    }

    public RoleForDatabaseRetrievingBuilder WithConcurrencyStamp(string roleConcurrencyStamp)
    {
        ConcurrencyStamp = roleConcurrencyStamp;

        return this;
    }

    public RoleForDatabaseRetrievingBuilder WithRemovedAt(DateTime roleRemovedAt)
    {
        RemovedAt = roleRemovedAt;

        return this;
    }

    public RoleForDatabaseRetrievingBuilder WithRemovedBy(Guid roleRemovedBy)
    {
        RemovedBy = roleRemovedBy;

        return this;
    }
}
