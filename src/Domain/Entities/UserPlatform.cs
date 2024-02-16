using Domain.Entities.Base;
using System;

namespace Domain.Entities;

/// <summary>
///     Represent the "UserPlatforms" table.
/// </summary>
public sealed class UserPlatform : IBaseEntity
{
    private UserPlatform()
    {
    }

    /// <summary>
    ///     Platform id.
    /// </summary>
    public Guid PlatformId { get; set; }

    /// <summary>
    ///     User id.
    /// </summary>
    public Guid UserId { get; set; }

    /// <summary>
    ///     User platform url.
    /// </summary>
    public string PlatformUrl { get; set; }

    // Navigation properties.
    public Platform Platform { get; set; }

    public User User { get; set; }

    /// <summary>
    ///     Return an instance.
    /// </summary>
    /// <param name="platformId">
    ///     Platform id of user platform.
    /// </param>
    /// <param name="platformUrl">
    ///     Platform url of user platform.
    /// </param>
    /// <param name="platform">
    ///     Platform of user platform.
    /// </param>
    /// <returns>
    ///     A new user platform object.
    /// </returns>
    public static UserPlatform InitVer1(
        Guid platformId,
        string platformUrl,
        Platform platform)
    {
        return new()
        {
            PlatformId = platformId,
            PlatformUrl = platformUrl,
            Platform = platform
        };
    }

    /// <summary>
    ///     Return an instance.
    /// </summary>
    /// <param name="userId">
    ///     Platform id of user platform.
    /// </param>
    /// <returns>
    ///     A new user platform object.
    /// </returns>
    public static UserPlatform InitVer2(Guid userId)
    {
        return new()
        {
            UserId = userId
        };
    }
}