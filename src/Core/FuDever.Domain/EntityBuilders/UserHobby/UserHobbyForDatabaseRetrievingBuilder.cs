using FuDever.Domain.EntityBuilders.UserHobby.Others;
using System;

namespace FuDever.Domain.EntityBuilders.UserHobby;

/// <summary>
///     User hobby for database retrieving builder.
/// </summary>
public sealed class UserHobbyForDatabaseRetrievingBuilder :
    Entities.UserHobby,
    IBaseUserHobbyBuilder,
    IUserHobbyBuilder<UserHobbyForDatabaseRetrievingBuilder>,
    IUserHobbyNavigationPropertyBuilder<UserHobbyForDatabaseRetrievingBuilder>
{
    public void Clear()
    {
        UserId = Guid.Empty;
        HobbyId = Guid.Empty;
        Hobby = default;
    }

    public Entities.UserHobby Complete()
    {
        return new()
        {
            UserId = UserId,
            HobbyId = HobbyId,
            Hobby = Hobby,
        };
    }

    public UserHobbyForDatabaseRetrievingBuilder WithHobby(Entities.Hobby hobby)
    {
        Hobby = hobby;

        return this;
    }

    public UserHobbyForDatabaseRetrievingBuilder WithHobbyId(Guid hobbyId)
    {
        HobbyId = hobbyId;

        return this;
    }

    public UserHobbyForDatabaseRetrievingBuilder WithUserId(Guid userId)
    {
        UserId = userId;

        return this;
    }
}
