using Domain.Entities.Base;
using System;
using System.Collections.Generic;

namespace Domain.Entities;

/// <summary>
///     Represent the "Platforms" table.
/// </summary>
public sealed class Platform :
    IBaseEntity,
    ITemporarilyRemovedEntity
{
    private Platform()
    {
    }

    /// <summary>
    ///     Platform id.
    /// </summary>
    public Guid Id { get; set; }

    /// <summary>
    ///     Platform name.
    /// </summary>
    public string Name { get; set; }

    public DateTime RemovedAt { get; set; }

    public Guid RemovedBy { get; set; }

    // Navigation collections.
    public IEnumerable<UserPlatform> UserPlatforms { get; set; }

    /// <summary>
    ///     Return an instance.
    /// </summary>
    /// <param name="platformId">
    ///     Id of platform.
    /// </param>
    /// <param name="platformName">
    ///     Platform name.
    /// </param>
    /// <param name="platformRemovedAt">
    ///     Platform is removed by whom.
    /// </param>
    /// <param name="platformRemovedBy">
    ///     When is platform removed.
    /// </param>
    /// <returns>
    ///     A new platform object.
    /// </returns>
    public static Platform InitVer1(
        Guid platformId,
        string platformName,
        DateTime platformRemovedAt,
        Guid platformRemovedBy)
    {
        // Validate platform name.
        if (string.IsNullOrWhiteSpace(value: platformName) ||
            platformName.Length > Metadata.Name.MaxLength ||
            platformName.Length < Metadata.Name.MinLength)
        {
            return default;
        }

        // Validate platform Id.
        if (platformId == Guid.Empty)
        {
            return default;
        }

        // Validate platform removed by.
        if (platformRemovedBy == Guid.Empty)
        {
            return default;
        }

        return new()
        {
            Id = platformId,
            Name = platformName,
            RemovedAt = platformRemovedAt,
            RemovedBy = platformRemovedBy
        };
    }

    /// <summary>
    ///     Return an instance.
    /// </summary>
    /// <param name="platformId">
    ///     Id of platform.
    /// </param>
    /// <param name="platformName">
    ///     Platform name.
    /// </param>
    /// <returns>
    ///     A new platform object.
    /// </returns>
    public static Platform InitVer2(
        Guid platformId,
        string platformName)
    {
        // Validate platform name.
        if (string.IsNullOrWhiteSpace(value: platformName) ||
            platformName.Length > Metadata.Name.MaxLength ||
            platformName.Length < Metadata.Name.MinLength)
        {
            return default;
        }

        // Validate platform Id.
        if (platformId == Guid.Empty)
        {
            return default;
        }

        return new()
        {
            Id = platformId,
            Name = platformName
        };
    }

    /// <summary>
    ///     Return an instance.
    /// </summary>
    /// <param name="platformName">
    ///     Platform name.
    /// </param>
    /// <returns>
    ///     A new platform object.
    /// </returns>
    public static Platform InitVer3(string platformName)
    {
        // Validate platform name.
        if (string.IsNullOrWhiteSpace(value: platformName) ||
            platformName.Length > Metadata.Name.MaxLength ||
            platformName.Length < Metadata.Name.MinLength)
        {
            return default;
        }

        return new()
        {
            Name = platformName
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
            public const int MaxLength = 100;

            /// <summary>
            ///     Min value length.
            /// </summary>
            public const int MinLength = 2;
        }
    }
}