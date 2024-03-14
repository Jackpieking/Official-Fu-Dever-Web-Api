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

    /// <summary>
    ///     Return an instance.
    /// </summary>
    /// <param name="roleId">
    ///     Id of role.
    /// </param>
    /// <param name="roleName">
    ///     Role name.
    /// </param>
    /// <param name="roleRemovedAt">
    ///     Role is removed by whom.
    /// </param>
    /// <param name="roleRemovedBy">
    ///     When is role removed.
    /// </param>
    /// <returns>
    ///     A new role object.
    /// </returns>
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
    ///     Return an instance.
    /// </summary>
    /// <param name="roleId">
    ///     Id of role.
    /// </param>
    /// <param name="roleName">
    ///     Role name.
    /// </param>
    /// <returns>
    ///     A new role object.
    /// </returns>
    public static Role InitVer2(
        Guid roleId,
        string roleName)
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

        return new()
        {
            Id = roleId,
            Name = roleName
        };
    }

    /// <summary>
    ///     Represent metadata of property.
    /// </summary>
    public static class Metadata
    {
        /// <summary>
        ///     Name property.
        /// </summary>
        public static class Name
        {
            /// <summary>
            ///     Max value length.
            /// </summary>
            public const int MaxLength = 50;

            /// <summary>
            ///     Min value length.
            /// </summary>
            public const int MinLength = 2;
        }
    }
}