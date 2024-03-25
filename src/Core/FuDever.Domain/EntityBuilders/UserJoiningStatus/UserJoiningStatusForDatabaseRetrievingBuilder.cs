using FuDever.Domain.EntityBuilders.UserJoiningStatus.Others;
using System;

namespace FuDever.Domain.EntityBuilders.UserJoiningStatus;

/// <summary>
///     User joining status for database retrieving builder.
/// </summary>
public sealed class UserJoiningStatusForDatabaseRetrievingBuilder :
    Entities.UserJoiningStatus,
    IBaseUserJoiningStatusBuilder,
    IUserJoiningStatusBuilder<UserJoiningStatusForDatabaseRetrievingBuilder>
{
    public Entities.UserJoiningStatus Complete()
    {
        return new()
        {
            Id = Id,
            Type = Type,
            RemovedAt = RemovedAt,
            RemovedBy = RemovedBy
        };
    }

    public UserJoiningStatusForDatabaseRetrievingBuilder WithId(Guid userJoiningStatusId)
    {
        Id = userJoiningStatusId;

        return this;
    }

    public UserJoiningStatusForDatabaseRetrievingBuilder WithType(string userJoiningStatusType)
    {
        Type = userJoiningStatusType;

        return this;
    }

    public UserJoiningStatusForDatabaseRetrievingBuilder WithRemovedAt(DateTime userJoiningStatusRemovedAt)
    {
        RemovedAt = userJoiningStatusRemovedAt;

        return this;
    }

    public UserJoiningStatusForDatabaseRetrievingBuilder WithRemovedBy(Guid userJoiningStatusRemovedBy)
    {
        RemovedBy = userJoiningStatusRemovedBy;

        return this;
    }
}
