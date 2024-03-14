using FuDever.Domain.Entities.Base;
using Microsoft.AspNetCore.Identity;
using System;

namespace FuDever.Domain.Entities;

/// <summary>
///     Represent the "UserTokens" table.
/// </summary>
public sealed class UserToken :
    IdentityUserToken<Guid>,
    IBaseEntity
{
}