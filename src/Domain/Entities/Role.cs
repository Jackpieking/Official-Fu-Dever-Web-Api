using Domain.Entities.Base;
using Microsoft.AspNetCore.Identity;
using System;

namespace Domain.Entities;

/// <summary>
///     Represent the "Roles" table.
/// </summary>
public sealed class Role :
    IdentityRole<Guid>,
    IBaseEntity,
    ITemporarilyRemovedEntity
{
    public DateTime RemovedAt { get; set; }

    public Guid RemovedBy { get; set; }
}