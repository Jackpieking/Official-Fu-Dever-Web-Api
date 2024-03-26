using FuDever.Domain.EntityBuilders.Platform.Others;
using System;

namespace FuDever.Domain.EntityBuilders.Platform;

/// <summary>
///     Platform for database retrieving builder.
/// </summary>
public sealed class PlatformForDatabaseRetrievingBuilder :
    Entities.Platform,
    IBasePlatformBuilder,
    IPlatformBuilder<PlatformForDatabaseRetrievingBuilder>
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

    public PlatformForDatabaseRetrievingBuilder WithId(Guid platformId)
    {
        Id = platformId;

        return this;
    }

    public PlatformForDatabaseRetrievingBuilder WithName(string platformName)
    {
        Name = platformName;

        return this;
    }

    public PlatformForDatabaseRetrievingBuilder WithRemovedAt(DateTime platformRemovedAt)
    {
        RemovedAt = platformRemovedAt;

        return this;
    }

    public PlatformForDatabaseRetrievingBuilder WithRemovedBy(Guid platformRemovedBy)
    {
        RemovedBy = platformRemovedBy;

        return this;
    }
}
