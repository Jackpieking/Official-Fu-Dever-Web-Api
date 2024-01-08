using System;
using System.Collections.Generic;
using Domain.Entities.Base;

namespace Domain.Entities;

/// <summary>
///     Represent the "UserJoiningStatus" table.
/// </summary>
public sealed class UserJoiningStatus :
    IBaseEntity
{
    /// <summary>
    ///     User joining status id.
    /// </summary>
    public Guid Id { get; set; }

    /// <summary>
    ///     User joining status type.
    /// </summary>
    public string Type { get; set; }

    public DateTime RemovedAt { get; set; }

    public Guid RemovedBy { get; set; }

    // Navigation collections.
    public IEnumerable<User> Users { get; set; }
}