using System;
using System.Collections.Generic;
using Domain.Entities.Base;

namespace Domain.Entities;

/// <summary>
///     Represent the "Departments" table.
/// </summary>
public sealed class Department :
    IBaseEntity,
    ITemporarilyRemovedEntity
{
    /// <summary>
    ///     Department id.
    /// </summary>
    public Guid Id { get; set; }

    /// <summary>
    ///     Department name.
    /// </summary>
    public string Name { get; set; }

    public DateTime RemovedAt { get; set; }

    public Guid RemovedBy { get; set; }

    // Navigation collections.
    public IEnumerable<User> Users { get; set; }
}