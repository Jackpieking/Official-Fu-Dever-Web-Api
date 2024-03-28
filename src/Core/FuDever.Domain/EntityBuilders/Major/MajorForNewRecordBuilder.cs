using System;
using FuDever.Domain.EntityBuilders.Major.Others;

namespace FuDever.Domain.EntityBuilders.Major;

/// <summary>
///     Major for new record builder.
/// </summary>
public sealed class MajorForNewRecordBuilder :
    Entities.Major,
    IBaseMajorBuilder,
    IMajorBuilder<MajorForNewRecordBuilder>
{
    public void Clear()
    {
        Id = Guid.Empty;
        Name = default;
        RemovedAt = default;
        RemovedBy = Guid.Empty;
    }

    public MajorForNewRecordBuilder WithId(Guid majorId)
    {
        // Validate major Id.
        if (majorId == Guid.Empty)
        {
            return default;
        }

        Id = majorId;

        return this;
    }

    public MajorForNewRecordBuilder WithName(string majorName)
    {
        // Validate major name.
        if (string.IsNullOrWhiteSpace(value: majorName) ||
            majorName.Length > Metadata.Name.MaxLength ||
            majorName.Length < Metadata.Name.MinLength)
        {
            return default;
        }

        Name = majorName;

        return this;
    }

    public MajorForNewRecordBuilder WithRemovedAt(DateTime majorRemovedAt)
    {
        // Validate major removed at.
        if (majorRemovedAt == DateTime.MaxValue ||
            majorRemovedAt.Kind != DateTimeKind.Utc)
        {
            return default;
        }

        RemovedAt = majorRemovedAt;

        return this;
    }

    public MajorForNewRecordBuilder WithRemovedBy(Guid majorRemovedBy)
    {
        // Validate major removed by.
        if (majorRemovedBy == Guid.Empty)
        {
            return default;
        }

        RemovedBy = majorRemovedBy;

        return this;
    }

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
}
