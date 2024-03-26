using FuDever.Domain.EntityBuilders.Major.Others;
using System;

namespace FuDever.Domain.EntityBuilders.Major;

/// <summary>
///     Major for database retrieving builder.
/// </summary>
public sealed class MajorForDatabaseRetrievingBuilder :
    Entities.Major,
    IBaseMajorBuilder,
    IMajorBuilder<MajorForDatabaseRetrievingBuilder>
{
    public void Clear()
    {
        Id = Guid.Empty;
        Name = default;
        RemovedAt = default;
        RemovedBy = Guid.Empty;
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

    public MajorForDatabaseRetrievingBuilder WithId(Guid majorId)
    {
        Id = majorId;

        return this;
    }

    public MajorForDatabaseRetrievingBuilder WithName(string majorName)
    {
        Name = majorName;

        return this;
    }

    public MajorForDatabaseRetrievingBuilder WithRemovedAt(DateTime majorRemovedAt)
    {
        RemovedAt = majorRemovedAt;

        return this;
    }

    public MajorForDatabaseRetrievingBuilder WithRemovedBy(Guid majorRemovedBy)
    {
        RemovedBy = majorRemovedBy;

        return this;
    }
}
