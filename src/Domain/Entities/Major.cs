using System;
using System.Collections.Generic;
using Domain.Entities.Base;

namespace Domain.Entities;

/// <summary>
///     Represent the "Majors" table.
/// </summary>
public sealed class Major :
    IBaseEntity,
    ITemporarilyRemovedEntity
{
    /// <summary>
    ///     Major id.
    /// </summary>
    public Guid Id { get; set; }

    /// <summary>
    ///     Major name.
    /// </summary>
    public string Name { get; set; }

    public DateTime RemovedAt { get; set; }

    public Guid RemovedBy { get; set; }

    // Navigation collections.
    public IEnumerable<User> Users { get; set; }
}