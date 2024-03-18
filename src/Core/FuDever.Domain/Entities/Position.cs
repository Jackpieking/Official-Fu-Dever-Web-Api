using FuDever.Domain.Entities.Base;
using System;
using System.Collections.Generic;

namespace FuDever.Domain.Entities;

/// <summary>
///     Represent the "Positions" table.
/// </summary>
public sealed class Position :
    IBaseEntity,
    ITemporarilyRemovedEntity
{
    private Position()
    {
    }

    public Guid Id { get; set; }

    public string Name { get; set; }

    public DateTime RemovedAt { get; set; }

    public Guid RemovedBy { get; set; }

    // Navigation collections.
    public IEnumerable<User> Users { get; set; }

    public static Position InitForSeeding(
        Guid positionId,
        string positionName,
        DateTime positionRemovedAt,
        Guid positionRemovedBy)
    {
        return new()
        {
            Id = positionId,
            Name = positionName,
            RemovedAt = positionRemovedAt,
            RemovedBy = positionRemovedBy
        };
    }

    public static Position InitFromDatabaseVer1(string positionName)
    {
        return new()
        {
            Name = positionName,
        };
    }

    public static Position InitFromDatabaseVer2(
        Guid positionId,
        string positionName)
    {
        return new()
        {
            Id = positionId,
            Name = positionName
        };
    }

    public static Position InitFromDatabaseVer3(
        Guid positionId,
        string positionName,
        DateTime positionRemovedAt,
        Guid positionRemovedBy)
    {
        return new()
        {
            Id = positionId,
            Name = positionName,
            RemovedAt = positionRemovedAt,
            RemovedBy = positionRemovedBy
        };
    }

    public static Position InitVer1(
        Guid positionId,
        string positionName,
        DateTime positionRemovedAt,
        Guid positionRemovedBy)
    {
        // Validate position name.
        if (string.IsNullOrWhiteSpace(value: positionName) ||
            positionName.Length > Metadata.Name.MaxLength ||
            positionName.Length < Metadata.Name.MinLength)
        {
            return default;
        }

        // Validate position Id.
        if (positionId == Guid.Empty)
        {
            return default;
        }

        // Validate position removed by.
        if (positionRemovedBy == Guid.Empty)
        {
            return default;
        }

        return new()
        {
            Id = positionId,
            Name = positionName,
            RemovedAt = positionRemovedAt,
            RemovedBy = positionRemovedBy
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