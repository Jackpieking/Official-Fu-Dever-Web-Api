using Domain.Entities.Base;
using Microsoft.AspNetCore.Identity;
using System;

namespace Domain.Entities;

/// <summary>
///     Represent the "RoleClaims" table.
/// </summary>
public sealed class RoleClaim :
    IdentityRoleClaim<Guid>,
    IBaseEntity
{
}