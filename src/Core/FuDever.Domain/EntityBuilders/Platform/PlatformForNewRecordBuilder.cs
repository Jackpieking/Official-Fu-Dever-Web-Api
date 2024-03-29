﻿using System;
using FuDever.Domain.EntityBuilders.Platform.Others;

namespace FuDever.Domain.EntityBuilders.Platfrom;

/// <summary>
///     Platform for new record builder.
/// </summary>
public sealed class PlatformForNewRecordBuilder :
    Entities.Platform,
    IBasePlatformBuilder,
    IPlatformBuilder<PlatformForNewRecordBuilder>
{
    public void Clear()
    {
        Id = Guid.Empty;
        Name = default;
        RemovedAt = default;
        RemovedBy = Guid.Empty;
    }

    public PlatformForNewRecordBuilder WithId(Guid platformId)
    {
        // Validate platform Id.
        if (platformId == Guid.Empty)
        {
            return default;
        }

        Id = platformId;

        return this;
    }

    public PlatformForNewRecordBuilder WithName(string platformName)
    {
        // Validate platform name.
        if (string.IsNullOrWhiteSpace(value: platformName) ||
            platformName.Length > Metadata.Name.MaxLength ||
            platformName.Length < Metadata.Name.MinLength)
        {
            return default;
        }

        Name = platformName;

        return this;
    }

    public PlatformForNewRecordBuilder WithRemovedAt(DateTime platformRemovedAt)
    {
        // Validate platform removed at.
        if (platformRemovedAt == DateTime.MaxValue ||
            platformRemovedAt.Kind != DateTimeKind.Utc)
        {
            return default;
        }

        RemovedAt = platformRemovedAt;

        return this;
    }

    public PlatformForNewRecordBuilder WithRemovedBy(Guid platformRemovedBy)
    {
        // Validate platform removed by.
        if (platformRemovedBy == Guid.Empty)
        {
            return default;
        }

        RemovedBy = platformRemovedBy;

        return this;
    }

    public Entities.Platform Complete()
    {
        return new()
        {
            Id = Id,
            Name = Name,
            RemovedAt = RemovedAt,
            RemovedBy = RemovedBy
        };
    }
}
