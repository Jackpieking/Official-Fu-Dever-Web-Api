﻿using System;
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
            majorName.Length > Entities.Major.Metadata.Name.MaxLength ||
            majorName.Length < Entities.Major.Metadata.Name.MinLength)
        {
            return default;
        }

        Name = majorName;

        return this;
    }

    public MajorForNewRecordBuilder WithRemovedAt(DateTime majorRemovedAt)
    {
        // Validate major removed at.
        if (majorRemovedAt == DateTime.MaxValue)
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
