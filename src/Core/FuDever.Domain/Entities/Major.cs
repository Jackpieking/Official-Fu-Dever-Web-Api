using FuDever.Domain.Entities.Base;
using System;
using System.Collections.Generic;

namespace FuDever.Domain.Entities;

/// <summary>
///     Represent the "Majors" table.
/// </summary>
public sealed class Major :
    IBaseEntity,
    ITemporarilyRemovedEntity
{
    private Major()
    {
    }

    public Guid Id { get; set; }

    public string Name { get; set; }

    public DateTime RemovedAt { get; set; }

    public Guid RemovedBy { get; set; }

    // Navigation collections.
    public IEnumerable<User> Users { get; set; }

    public static Major InitForSeeding(
        Guid majorId,
        string majorName,
        DateTime majorRemovedAt,
        Guid majorRemovedBy)
    {
        return new()
        {
            Id = majorId,
            Name = majorName,
            RemovedAt = majorRemovedAt,
            RemovedBy = majorRemovedBy
        };
    }

    public static Major InitFromDatabaseVer1(
        Guid majorId,
        string majorName)
    {
        return new()
        {
            Id = majorId,
            Name = majorName
        };
    }

    public static Major InitFromDatabaseVer2(string majorName)
    {
        return new()
        {
            Name = majorName
        };
    }

    public static Major InitFromDatabaseVer3(
        Guid majorId,
        string majorName,
        DateTime majorRemovedAt,
        Guid majorRemovedBy)
    {
        return new()
        {
            Id = majorId,
            Name = majorName,
            RemovedAt = majorRemovedAt,
            RemovedBy = majorRemovedBy
        };
    }

    public static Major InitVer1(
        Guid majorId,
        string majorName,
        DateTime majorRemovedAt,
        Guid majorRemovedBy)
    {
        // Validate major name.
        if (string.IsNullOrWhiteSpace(value: majorName) ||
            majorName.Length > Metadata.Name.MaxLength ||
            majorName.Length < Metadata.Name.MinLength)
        {
            return default;
        }

        // Validate major Id.
        if (majorId == Guid.Empty)
        {
            return default;
        }

        // Validate major removed by.
        if (majorRemovedBy == Guid.Empty)
        {
            return default;
        }

        return new()
        {
            Id = majorId,
            Name = majorName,
            RemovedAt = majorRemovedAt,
            RemovedBy = majorRemovedBy
        };
    }

    /// <summary>
    ///     Represent metadata of property.
    /// </summary>
    public static class Metadata
    {
        public static class Name
        {
            public const int MaxLength = 100;

            public const int MinLength = 2;
        }
    }
}