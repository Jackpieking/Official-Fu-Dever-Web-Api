using System;
using FuDever.Domain.EntityBuilders.Hobby.Others;

namespace FuDever.Domain.EntityBuilders.Hobby;

/// <summary>
///     Hobby for new record builder.
/// </summary>
public sealed class HobbyForNewRecordBuilder :
    Entities.Hobby,
    IBaseHobbyBuilder,
    IHobbyBuilder<HobbyForNewRecordBuilder>
{
    public void Clear()
    {
        Id = Guid.Empty;
        Name = default;
        RemovedAt = default;
        RemovedBy = Guid.Empty;
    }

    public HobbyForNewRecordBuilder WithId(Guid hobbyId)
    {
        // Validate hobby Id.
        if (hobbyId == Guid.Empty)
        {
            return default;
        }

        Id = hobbyId;

        return this;
    }

    public HobbyForNewRecordBuilder WithName(string hobbyName)
    {
        // Validate hobby name.
        if (string.IsNullOrWhiteSpace(value: hobbyName) ||
            hobbyName.Length > Entities.Hobby.Metadata.Name.MaxLength ||
            hobbyName.Length < Entities.Hobby.Metadata.Name.MinLength)
        {
            return default;
        }

        Name = hobbyName;

        return this;
    }

    public HobbyForNewRecordBuilder WithRemovedAt(DateTime hobbyRemovedAt)
    {
        // Validate hobby removed at.
        if (hobbyRemovedAt == DateTime.MaxValue)
        {
            return default;
        }

        RemovedAt = hobbyRemovedAt;

        return this;
    }

    public HobbyForNewRecordBuilder WithRemovedBy(Guid hobbyRemovedBy)
    {
        // Validate hobby removed by.
        if (hobbyRemovedBy == Guid.Empty)
        {
            return default;
        }

        RemovedBy = hobbyRemovedBy;

        return this;
    }

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
}
