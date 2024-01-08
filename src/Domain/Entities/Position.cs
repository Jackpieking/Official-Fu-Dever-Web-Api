using System;
using System.Collections.Generic;
using Domain.Entities.Base;

namespace Domain.Entities;

/// <summary>
///     Represent the "Positions" table.
/// </summary>
public sealed class Position :
    IBaseEntity,
    ITemporarilyRemovedEntity
{
    /// <summary>
    ///     Position id.
    /// </summary>
    public Guid Id { get; set; }

    /// <summary>
    ///     Position name.
    /// </summary>
    public string Name { get; set; }

    public DateTime RemovedAt { get; set; }

    public Guid RemovedBy { get; set; }

    // Navigation collections.
    public IEnumerable<User> Users { get; set; }
}