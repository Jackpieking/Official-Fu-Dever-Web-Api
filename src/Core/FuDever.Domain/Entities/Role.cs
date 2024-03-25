using FuDever.Domain.Entities.Base;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;

namespace FuDever.Domain.Entities;

/// <summary>
///     Represent the "Roles" table.
/// </summary>
public class Role :
    IdentityRole<Guid>,
    IBaseEntity,
    ITemporarilyRemovedEntity
{
    internal Role()
    {
    }

    public DateTime RemovedAt { get; set; }

    public Guid RemovedBy { get; set; }

    // Navigation collections.
    public IEnumerable<UserRole> UserRoles { get; set; }

    public IEnumerable<RoleClaim> RoleClaims { get; set; }

    public static class Metadata
    {
        public static class Name
        {
            public const int MaxLength = 50;

            public const int MinLength = 2;
        }
    }
}