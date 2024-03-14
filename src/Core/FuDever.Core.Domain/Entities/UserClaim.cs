using FuDever.Domain.Entities.Base;
using Microsoft.AspNetCore.Identity;
using System;

namespace FuDever.Domain.Entities;

/// <summary>
///     Represent the "UserClaims" table.
/// </summary>
public sealed class UserClaim :
    IdentityUserClaim<Guid>,
    IBaseEntity
{
}