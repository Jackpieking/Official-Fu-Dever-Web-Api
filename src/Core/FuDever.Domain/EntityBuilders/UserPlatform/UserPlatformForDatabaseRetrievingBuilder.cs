using FuDever.Domain.EntityBuilders.UserPlatform.Others;
using System;

namespace FuDever.Domain.EntityBuilders.UserPlatform;

/// <summary>
///     User platform for database retrieving builder.
/// </summary>
public sealed class UserPlatformForDatabaseRetrievingBuilder :
    Entities.UserPlatform,
    IBaseUserPlatformBuilder,
    IUserPlatformBuilder<UserPlatformForDatabaseRetrievingBuilder>,
    IUserPlatformNavigationPropertyBuilder<UserPlatformForDatabaseRetrievingBuilder>
{
    public Entities.UserPlatform Complete()
    {
        return new()
        {
            Platform = Platform,
            PlatformId = PlatformId,
            PlatformUrl = PlatformUrl,
            UserId = UserId
        };
    }

    public UserPlatformForDatabaseRetrievingBuilder WithPlatform(Entities.Platform platform)
    {
        Platform = platform;

        return this;
    }

    public UserPlatformForDatabaseRetrievingBuilder WithPlatformId(Guid platformId)
    {
        PlatformId = platformId;

        return this;
    }

    public UserPlatformForDatabaseRetrievingBuilder WithPlatformUrl(string platformUrl)
    {
        PlatformUrl = platformUrl;

        return this;
    }

    public UserPlatformForDatabaseRetrievingBuilder WithUserId(Guid userId)
    {
        UserId = userId;

        return this;
    }
}
