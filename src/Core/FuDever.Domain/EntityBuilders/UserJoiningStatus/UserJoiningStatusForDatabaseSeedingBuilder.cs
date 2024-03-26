using FuDever.Domain.EntityBuilders.UserJoiningStatus.Others;
using System;

namespace FuDever.Domain.EntityBuilders.UserJoiningStatus;

/// <summary>
///     User joining status for database seeding builder.
/// </summary>
public sealed class UserJoiningStatusForDatabaseSeedingBuilder :
    Entities.UserJoiningStatus,
    IBaseUserJoiningStatusBuilder,
    IUserJoiningStatusBuilder<UserJoiningStatusForDatabaseSeedingBuilder>
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

    public UserJoiningStatusForDatabaseSeedingBuilder WithId(Guid userJoiningStatusId)
    {
        // Validate userJoiningStatus Id.
        if (userJoiningStatusId == Guid.Empty)
        {
            return default;
        }

        Id = userJoiningStatusId;

        return this;
    }

    public UserJoiningStatusForDatabaseSeedingBuilder WithType(string userJoiningStatusType)
    {
        Type = userJoiningStatusType;

        return this;
    }

    public UserJoiningStatusForDatabaseSeedingBuilder WithRemovedAt(DateTime userJoiningStatusRemovedAt)
    {
        // Validate userJoiningStatus removed at.
        if (userJoiningStatusRemovedAt == DateTime.MaxValue)
        {
            return default;
        }

        RemovedAt = userJoiningStatusRemovedAt;

        return this;
    }

    public UserJoiningStatusForDatabaseSeedingBuilder WithRemovedBy(Guid userJoiningStatusRemovedBy)
    {
        // Validate userJoiningStatus removed by.
        if (userJoiningStatusRemovedBy == Guid.Empty)
        {
            return default;
        }

        RemovedBy = userJoiningStatusRemovedBy;

        return this;
    }

    public void Clear()
    {
        Id = Guid.Empty;
        Type = default;
        RemovedAt = default;
        RemovedBy = Guid.Empty;
    }
}
