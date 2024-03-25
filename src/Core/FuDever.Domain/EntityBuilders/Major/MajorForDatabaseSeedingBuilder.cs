using FuDever.Domain.EntityBuilders.Major.Others;
using System;

namespace FuDever.Domain.EntityBuilders.Major;

/// <summary>
///     Major for database seeding builder.
/// </summary>
public sealed class MajorForDatabaseSeedingBuilder :
    Entities.Major,
    IBaseMajorBuilder,
    IMajorBuilder<MajorForDatabaseSeedingBuilder>
{
    public Entities.Major Complete()
    {
        return new()
        {
            Id = Id,
            Name = Name,
            RemovedAt = RemovedAt,
            RemovedBy = RemovedBy
        };
    }

    public MajorForDatabaseSeedingBuilder WithId(Guid majorId)
    {
        // Validate major Id.
        if (majorId == Guid.Empty)
        {
            return default;
        }

        Id = majorId;

        return this;
    }

    public MajorForDatabaseSeedingBuilder WithName(string majorName)
    {
        Name = majorName;

        return this;
    }

    public MajorForDatabaseSeedingBuilder WithRemovedAt(DateTime majorRemovedAt)
    {
        // Validate major removed at.
        if (majorRemovedAt == DateTime.MaxValue)
        {
            return default;
        }

        RemovedAt = majorRemovedAt;

        return this;
    }

    public MajorForDatabaseSeedingBuilder WithRemovedBy(Guid majorRemovedBy)
    {
        // Validate major removed by.
        if (majorRemovedBy == Guid.Empty)
        {
            return default;
        }

        RemovedBy = majorRemovedBy;

        return this;
    }
}
