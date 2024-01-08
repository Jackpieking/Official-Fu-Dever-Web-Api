using Domain.Entities.Base;
using Microsoft.AspNetCore.Identity;
using System;

namespace Domain.Entities;

/// <summary>
///     Represent the "UserClaims" table.
/// </summary>
public sealed class UserClaim :
    IdentityUserClaim<Guid>,
    IBaseEntity
{
}