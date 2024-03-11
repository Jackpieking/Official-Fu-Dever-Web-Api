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
        // Validate platform url.
        if (string.IsNullOrWhiteSpace(value: platformUrl) ||
            platformUrl.Length > Metadata.PlatformUrl.MaxLength ||
            platformUrl.Length < Metadata.PlatformUrl.MinLength)
        {
            return default;
        }

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

    /// <summary>
    ///     Represent metadata of property.
    /// </summary>
    public static class Metadata
    {
        /// <summary>
        ///     Name property.
        /// </summary>
        public static class PlatformUrl
        {
            /// <summary>
            ///     Max value length.
            /// </summary>
            public const int MaxLength = 100;

            /// <summary>
            ///     Min value length.
            /// </summary>
            public const int MinLength = 2;
        }
    }
}