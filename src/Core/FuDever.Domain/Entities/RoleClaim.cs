using FuDever.Domain.Entities.Base;
using Microsoft.AspNetCore.Identity;
using System;

namespace FuDever.Domain.Entities;

/// <summary>
///     Represent the "RoleClaims" table.
/// </summary>
public class RoleClaim :
    IdentityRoleClaim<Guid>,
    IBaseEntity
{
    // Navigation properties.
    public Role Role { get; set; }
}