using System;
using System.Collections.Generic;
using Domain.Entities.Base;

namespace Domain.Entities;

/// <summary>
///     Represent the "Platforms" table.
/// </summary>
public sealed class Platform :
    IBaseEntity,
    ITemporarilyRemovedEntity
{
    /// <summary>
    ///     Platform id.
    /// </summary>
    public Guid Id { get; set; }

    /// <summary>
    ///     Platform name.
    /// </summary>
    public string Name { get; set; }

    public DateTime RemovedAt { get; set; }

    public Guid RemovedBy { get; set; }

    // Navigation collections.
    public IEnumerable<UserPlatform> UserPlatforms { get; set; }
}