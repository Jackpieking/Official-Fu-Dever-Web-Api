using System;
using Domain.Entities.Base;

namespace Domain.Entities;

/// <summary>
///     Represent the "UserPlatforms" table.
/// </summary>
public sealed class UserPlatform :
    IBaseEntity
{
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
}