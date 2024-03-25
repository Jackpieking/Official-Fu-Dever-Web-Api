using FuDever.Domain.Entities.Base;
using Microsoft.AspNetCore.Identity;
using System;

namespace FuDever.Domain.Entities;

/// <summary>
///     Represent the "UserClaims" table.
/// </summary>
public class UserClaim :
    IdentityUserClaim<Guid>,
    IBaseEntity
{
    // Navigation properties.
    public User User { get; set; }
}