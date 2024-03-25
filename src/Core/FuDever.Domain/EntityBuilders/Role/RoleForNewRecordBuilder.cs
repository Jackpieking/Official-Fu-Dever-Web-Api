using System;
using FuDever.Domain.EntityBuilders.Role.Others;

namespace FuDever.Domain.EntityBuilders.Role;

/// <summary>
///     Role for new record builder.
/// </summary>
public sealed class RoleForNewRecordBuilder :
    Entities.Role,
    IBaseRoleBuilder,
    IRoleBuilder<RoleForNewRecordBuilder>
{
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

    public RoleForNewRecordBuilder WithId(Guid roleId)
    {
        // Validate role Id.
        if (roleId == Guid.Empty)
        {
            return default;
        }

        Id = roleId;

        return this;
    }

    public RoleForNewRecordBuilder WithName(string roleName)
    {
        // Validate role name.
        if (string.IsNullOrWhiteSpace(value: roleName) ||
            roleName.Length > Entities.Role.Metadata.Name.MaxLength ||
            roleName.Length < Entities.Role.Metadata.Name.MinLength)
        {
            return default;
        }

        Name = roleName;

        return this;
    }

    public RoleForNewRecordBuilder WithNormalizedName(string roleNormalizedName)
    {
        NormalizedName = roleNormalizedName;

        return this;
    }

    public RoleForNewRecordBuilder WithConcurrencyStamp(string roleConcurrencyStamp)
    {
        ConcurrencyStamp = roleConcurrencyStamp;

        return this;
    }

    public RoleForNewRecordBuilder WithRemovedAt(DateTime roleRemovedAt)
    {
        // Validate role removed at.
        if (roleRemovedAt == DateTime.MaxValue)
        {
            return default;
        }

        RemovedAt = roleRemovedAt;

        return this;
    }

    public RoleForNewRecordBuilder WithRemovedBy(Guid roleRemovedBy)
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
