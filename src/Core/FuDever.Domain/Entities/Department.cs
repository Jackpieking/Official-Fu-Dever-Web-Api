using FuDever.Domain.Entities.Base;
using System;
using System.Collections.Generic;

namespace FuDever.Domain.Entities;

/// <summary>
///     Represent the "Departments" table.
/// </summary>
public sealed class Department :
    IBaseEntity,
    ITemporarilyRemovedEntity
{
    private Department()
    {
    }

    public Guid Id { get; set; }

    public string Name { get; set; }

    public DateTime RemovedAt { get; set; }

    public Guid RemovedBy { get; set; }

    // Navigation collections.
    public IEnumerable<User> Users { get; set; }

    public static Department InitForSeeding(
        Guid departmentId,
        string departmentName,
        DateTime departmentRemovedAt,
        Guid departmentRemovedBy)
    {
        return new()
        {
            Id = departmentId,
            Name = departmentName,
            RemovedAt = departmentRemovedAt,
            RemovedBy = departmentRemovedBy
        };
    }

    public static Department InitFromDatabaseVer1(
        Guid departmentId,
        string departmentName)
    {
        return new()
        {
            Id = departmentId,
            Name = departmentName
        };
    }

    public static Department InitFromDatabaseVer2(
        Guid departmentId,
        string departmentName,
        DateTime departmentRemovedAt,
        Guid departmentRemovedBy)
    {
        return new()
        {
            Id = departmentId,
            Name = departmentName,
            RemovedAt = departmentRemovedAt,
            RemovedBy = departmentRemovedBy
        };
    }

    public static Department InitFromDatabaseVer3(string departmentName)
    {
        return new()
        {
            Name = departmentName
        };
    }

    public static Department InitVer1(
        Guid departmentId,
        string departmentName,
        DateTime departmentRemovedAt,
        Guid departmentRemovedBy)
    {
        // Validate department name.
        if (string.IsNullOrWhiteSpace(value: departmentName) ||
            departmentName.Length > Metadata.Name.MaxLength ||
            departmentName.Length < Metadata.Name.MinLength)
        {
            return default;
        }

        // Validate department Id.
        if (departmentId == Guid.Empty)
        {
            return default;
        }

        // Validate department removed by.
        if (departmentRemovedBy == Guid.Empty)
        {
            return default;
        }

        return new()
        {
            Id = departmentId,
            Name = departmentName,
            RemovedAt = departmentRemovedAt,
            RemovedBy = departmentRemovedBy
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