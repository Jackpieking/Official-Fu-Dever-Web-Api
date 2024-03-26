using System;
using FuDever.Domain.EntityBuilders.Position.Others;

namespace FuDever.Domain.EntityBuilders.Position;

/// <summary>
///     Position for new record builder.
/// </summary>
public sealed class PositionForNewRecordBuilder :
    Entities.Position,
    IBasePositionBuilder,
    IPositionBuilder<PositionForNewRecordBuilder>
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

    public PositionForNewRecordBuilder WithId(Guid positionId)
    {
        // Validate position Id.
        if (positionId == Guid.Empty)
        {
            return default;
        }

        Id = positionId;

        return this;
    }

    public PositionForNewRecordBuilder WithName(string positionName)
    {
        // Validate position name.
        if (string.IsNullOrWhiteSpace(value: positionName) ||
            positionName.Length > Entities.Position.Metadata.Name.MaxLength ||
            positionName.Length < Entities.Position.Metadata.Name.MinLength)
        {
            return default;
        }

        Name = positionName;

        return this;
    }

    public PositionForNewRecordBuilder WithRemovedAt(DateTime positionRemovedAt)
    {
        // Validate position removed at.
        if (positionRemovedAt == DateTime.MaxValue)
        {
            return default;
        }

        RemovedAt = positionRemovedAt;

        return this;
    }

    public PositionForNewRecordBuilder WithRemovedBy(Guid positionRemovedBy)
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
