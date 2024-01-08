using System;
using System.Collections.Generic;
using Domain.Entities.Base;

namespace Domain.Entities;

/// <summary>
///     Represent the "Hobbies" table.
/// </summary>
public sealed class Hobby :
    IBaseEntity,
    ITemporarilyRemovedEntity
{
    /// <summary>
    ///     Hobby id.
    /// </summary>
    public Guid Id { get; set; }

    /// <summary>
    ///     Hobby name.
    /// </summary>
    public string Name { get; set; }

    public DateTime RemovedAt { get; set; }

    public Guid RemovedBy { get; set; }

    // Navigation collections.
    public IEnumerable<UserHobby> UserHobbies { get; set; }
}