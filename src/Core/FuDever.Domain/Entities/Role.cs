using FuDever.Domain.Entities.Base;
using Microsoft.AspNetCore.Identity;
using System;

namespace FuDever.Domain.Entities;

/// <summary>
///     Represent the "Roles" table.
/// </summary>
public sealed class Role :
    IdentityRole<Guid>,
    IBaseEntity,
    ITemporarilyRemovedEntity
{
    private Role()
    {
    }

    public DateTime RemovedAt { get; set; }

    public Guid RemovedBy { get; set; }

    public static Role InitFromDatabaseVer1(
        Guid roleId,
        string roleName)
    {
        return new()
        {
            Id = roleId,
            Name = roleName
        };
    }

    public static Role InitFromDatabaseVer2(string roleName)
    {
        return new()
        {
            Name = roleName
        };
    }

    public static Role InitFromDatabaseVer3(
        Guid roleId,
        string roleName,
        DateTime roleRemovedAt,
        Guid roleRemovedBy)
    {
        return new()
        {
            Id = roleId,
            Name = roleName,
            RemovedAt = roleRemovedAt,
            RemovedBy = roleRemovedBy
        };
    }

    public static Role InitVer1(
        Guid roleId,
        string roleName,
        DateTime roleRemovedAt,
        Guid roleRemovedBy)
    {
        // Validate role type.
        if (string.IsNullOrWhiteSpace(value: roleName) ||
            roleName.Length > Metadata.Name.MaxLength ||
            roleName.Length < Metadata.Name.MinLength)
        {
            return default;
        }

        // Validate role Id.
        if (roleId == Guid.Empty)
        {
            return default;
        }

        // Validate role removed by.
        if (roleRemovedBy == Guid.Empty)
        {
            return default;
        }

        return new()
        {
            Id = roleId,
            Name = roleName,
            RemovedAt = roleRemovedAt,
            RemovedBy = roleRemovedBy
        };
    }

    /// <summary>
    ///     Represent metadata of property.
    /// </summary>
    public static class Metadata
    {
        public static class Name
        {
            public const int MaxLength = 50;

            public const int MinLength = 2;
        }
    }
}