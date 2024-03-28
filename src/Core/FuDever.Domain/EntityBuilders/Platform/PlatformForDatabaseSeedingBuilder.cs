using FuDever.Domain.EntityBuilders.Platform.Others;
using System;

namespace FuDever.Domain.EntityBuilders.Platform;

/// <summary>
///     Platform for database seeding builder.
/// </summary>
public sealed class PlatformForDatabaseSeedingBuilder :
    Entities.Platform,
    IBasePlatformBuilder,
    IPlatformBuilder<PlatformForDatabaseSeedingBuilder>
{
    public void Clear()
    {
        Id = Guid.Empty;
        Name = default;
        RemovedAt = default;
        RemovedBy = Guid.Empty;
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

    public PlatformForDatabaseSeedingBuilder WithId(Guid platformId)
    {
        // Validate platform Id.
        if (platformId == Guid.Empty)
        {
            return default;
        }

        Id = platformId;

        return this;
    }

    public PlatformForDatabaseSeedingBuilder WithName(string platformName)
    {
        Name = platformName;

        return this;
    }

    public PlatformForDatabaseSeedingBuilder WithRemovedAt(DateTime platformRemovedAt)
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

    public PlatformForDatabaseSeedingBuilder WithRemovedBy(Guid platformRemovedBy)
    {
        // Validate platform removed by.
        if (platformRemovedBy == Guid.Empty)
        {
            return default;
        }

        RemovedBy = platformRemovedBy;

        return this;
    }
}
