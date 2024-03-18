using FuDever.Domain.Entities.Base;
using System;

namespace FuDever.Domain.Entities;

/// <summary>
///     Represent the "UserPlatforms" table.
/// </summary>
public sealed class UserPlatform : IBaseEntity
{
    private UserPlatform()
    {
    }

    public Guid PlatformId { get; set; }

    public Guid UserId { get; set; }

    public string PlatformUrl { get; set; }

    // Navigation properties.
    public Platform Platform { get; set; }

    public User User { get; set; }

    public static UserPlatform InitFromDatabaseVer1(
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

    public static UserPlatform InitFromDatabaseVer2(Guid userId)
    {
        return new()
        {
            UserId = userId
        };
    }

    /// <summary>
    ///     Represent metadata of property.
    /// </summary>
    public static class Metadata
    {
        public static class PlatformUrl
        {
            public const int MaxLength = 100;

            public const int MinLength = 2;
        }
    }
}