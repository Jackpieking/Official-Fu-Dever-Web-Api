using FuDever.Domain.EntityBuilders.Hobby.Others;
using System;

namespace FuDever.Domain.EntityBuilders.Hobby;

/// <summary>
///     Hobby for database retrieving builder.
/// </summary>
public sealed class HobbyForDatabaseRetrievingBuilder :
    Entities.Hobby,
    IBaseHobbyBuilder,
    IHobbyBuilder<HobbyForDatabaseRetrievingBuilder>
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

    public HobbyForDatabaseRetrievingBuilder WithId(Guid hobbyId)
    {
        Id = hobbyId;

        return this;
    }

    public HobbyForDatabaseRetrievingBuilder WithName(string hobbyName)
    {
        Name = hobbyName;

        return this;
    }

    public HobbyForDatabaseRetrievingBuilder WithRemovedAt(DateTime hobbyRemovedAt)
    {
        RemovedAt = hobbyRemovedAt;

        return this;
    }

    public HobbyForDatabaseRetrievingBuilder WithRemovedBy(Guid hobbyRemovedBy)
    {
        RemovedBy = hobbyRemovedBy;

        return this;
    }
}
