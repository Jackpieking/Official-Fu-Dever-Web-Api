using FuDever.Domain.EntityBuilders.Hobby.Others;
using System;

namespace FuDever.Domain.EntityBuilders.Hobby;

/// <summary>
///     Hobby for database seeding builder.
/// </summary>
public sealed class HobbyForDatabaseSeedingBuilder :
    Entities.Hobby,
    IBaseHobbyBuilder,
    IHobbyBuilder<HobbyForDatabaseSeedingBuilder>
{
    public Entities.Hobby Complete()
    {
        return new()
        {
            Id = Id,
            Name = Name,
            RemovedAt = RemovedAt,
            RemovedBy = RemovedBy
        };
    }

    public HobbyForDatabaseSeedingBuilder WithId(Guid hobbyId)
    {
        // Validate hobby Id.
        if (hobbyId == Guid.Empty)
        {
            return default;
        }

        Id = hobbyId;

        return this;
    }

    public HobbyForDatabaseSeedingBuilder WithName(string hobbyName)
    {
        Name = hobbyName;

        return this;
    }

    public HobbyForDatabaseSeedingBuilder WithRemovedAt(DateTime hobbyRemovedAt)
    {
        // Validate hobby removed at.
        if (hobbyRemovedAt == DateTime.MaxValue)
        {
            return default;
        }

        RemovedAt = hobbyRemovedAt;

        return this;
    }

    public HobbyForDatabaseSeedingBuilder WithRemovedBy(Guid hobbyRemovedBy)
    {
        // Validate hobby removed by.
        if (hobbyRemovedBy == Guid.Empty)
        {
            return default;
        }

        RemovedBy = hobbyRemovedBy;

        return this;
    }
}
