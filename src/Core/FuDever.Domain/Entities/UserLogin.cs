using FuDever.Domain.Entities.Base;
using Microsoft.AspNetCore.Identity;
using System;

namespace FuDever.Domain.Entities;

/// <summary>
///     Represent the "UserLogins" table.
/// </summary>
public class UserLogin :
    IdentityUserLogin<Guid>,
    IBaseEntity
{
    // Navigation properties.
    public User User { get; set; }
}