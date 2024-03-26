using FuDever.Domain.EntityBuilders.Position.Others;
using System;

namespace FuDever.Domain.EntityBuilders.Position;

/// <summary>
///     Position for database seeding builder.
/// </summary>
public sealed class PositionForDatabaseSeedingBuilder :
    Entities.Position,
    IBasePositionBuilder,
    IPositionBuilder<PositionForDatabaseSeedingBuilder>
{
    public void Clear()
    {
        Id = Guid.Empty;
        Name = default;
        RemovedAt = default;
        RemovedBy = Guid.Empty;
    }

    public Entities.Position Complete()
    {
        return new()
        {
            Id = Id,
            Name = Name,
            RemovedAt = RemovedAt,
            RemovedBy = RemovedBy
        };
    }

    public PositionForDatabaseSeedingBuilder WithId(Guid positionId)
    {
        // Validate position Id.
        if (positionId == Guid.Empty)
        {
            return default;
        }

        Id = positionId;

        return this;
    }

    public PositionForDatabaseSeedingBuilder WithName(string positionName)
    {
        Name = positionName;

        return this;
    }

    public PositionForDatabaseSeedingBuilder WithRemovedAt(DateTime positionRemovedAt)
    {
        // Validate position removed at.
        if (positionRemovedAt == DateTime.MaxValue)
        {
            return default;
        }

        RemovedAt = positionRemovedAt;

        return this;
    }

    public PositionForDatabaseSeedingBuilder WithRemovedBy(Guid positionRemovedBy)
    {
        // Validate position removed by.
        if (positionRemovedBy == Guid.Empty)
        {
            return default;
        }

        RemovedBy = positionRemovedBy;

        return this;
    }
}
