using FuDever.Domain.EntityBuilders.Position.Others;
using System;

namespace FuDever.Domain.EntityBuilders.Position;

/// <summary>
///     Position for database retrieving builder.
/// </summary>
public sealed class PositionForDatabaseRetrievingBuilder :
    Entities.Position,
    IBasePositionBuilder,
    IPositionBuilder<PositionForDatabaseRetrievingBuilder>
{
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

    public PositionForDatabaseRetrievingBuilder WithId(Guid positionId)
    {
        Id = positionId;

        return this;
    }

    public PositionForDatabaseRetrievingBuilder WithName(string positionName)
    {
        Name = positionName;

        return this;
    }

    public PositionForDatabaseRetrievingBuilder WithRemovedAt(DateTime positionRemovedAt)
    {
        RemovedAt = positionRemovedAt;

        return this;
    }

    public PositionForDatabaseRetrievingBuilder WithRemovedBy(Guid positionRemovedBy)
    {
        RemovedBy = positionRemovedBy;

        return this;
    }
}
