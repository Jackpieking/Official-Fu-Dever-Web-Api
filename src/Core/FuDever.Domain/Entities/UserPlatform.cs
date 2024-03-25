using FuDever.Domain.Entities.Base;
using System;

namespace FuDever.Domain.Entities;

/// <summary>
///     Represent the "UserPlatforms" table.
/// </summary>
public class UserPlatform : IBaseEntity
{
    internal UserPlatform()
    {
    }

    public Guid PlatformId { get; set; }

    public Guid UserId { get; set; }

    public string PlatformUrl { get; set; }

    // Navigation properties.
    public Platform Platform { get; set; }

    public User User { get; set; }

    public static class Metadata
    {
        public static class PlatformUrl
        {
            public const int MaxLength = 100;

            public const int MinLength = 2;
        }
    }
}