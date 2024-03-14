using FuDever.Domain.Entities.Base;
using Microsoft.AspNetCore.Identity;
using System;

namespace FuDever.Domain.Entities;

/// <summary>
///     Represent the "UserRoles" table.
/// </summary>
public sealed class UserRole :
    IdentityUserRole<Guid>,
    IBaseEntity
{
}