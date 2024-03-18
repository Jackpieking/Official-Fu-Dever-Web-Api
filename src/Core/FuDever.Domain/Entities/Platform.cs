using FuDever.Domain.Entities.Base;
using System;
using System.Collections.Generic;

namespace FuDever.Domain.Entities;

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

    public Guid Id { get; set; }

    public string Name { get; set; }

    public DateTime RemovedAt { get; set; }

    public Guid RemovedBy { get; set; }

    // Navigation collections.
    public IEnumerable<UserPlatform> UserPlatforms { get; set; }

    public static Platform InitForSeeding(
        Guid platformId,
        string platformName,
        DateTime platformRemovedAt,
        Guid platformRemovedBy)
    {
        return new()
        {
            Id = platformId,
            Name = platformName,
            RemovedAt = platformRemovedAt,
            RemovedBy = platformRemovedBy
        };
    }

    public static Platform InitFromDatabaseVer1(
        Guid platformId,
        string platformName)
    {
        return new()
        {
            Id = platformId,
            Name = platformName
        };
    }

    public static Platform InitFromDatabaseVer2(string platformName)
    {
        return new()
        {
            Name = platformName
        };
    }

    public static Platform InitFromDatabaseVer3(
        Guid platformId,
        string platformName,
        DateTime platformRemovedAt,
        Guid platformRemovedBy)
    {
        return new()
        {
            Id = platformId,
            Name = platformName,
            RemovedAt = platformRemovedAt,
            RemovedBy = platformRemovedBy
        };
    }

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

    public static class Metadata
    {
        public static class Name
        {
            public const int MaxLength = 100;

            public const int MinLength = 2;
        }
    }
}