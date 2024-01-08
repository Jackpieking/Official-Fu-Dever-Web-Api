using Domain.Entities.Base;
using Microsoft.AspNetCore.Identity;
using System;

namespace Domain.Entities;

/// <summary>
///     Represent the "UserRoles" table.
/// </summary>
public sealed class UserRole :
    IdentityUserRole<Guid>,
    IBaseEntity
{
}