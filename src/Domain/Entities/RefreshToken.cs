using System;
using Domain.Entities.Base;

namespace Domain.Entities;

/// <summary>
///     Represent the "RefreshTokens" table.
/// </summary>
public sealed class RefreshToken :
    IBaseEntity,
    ICreatedEntity
{
    /// <summary>
    ///     Refresh token id.
    /// </summary>
    public Guid Id { get; set; }

    /// <summary>
    ///     User id.
    /// </summary>
    public Guid UserId { get; set; }

    /// <summary>
    ///     Refresh token value.
    /// </summary>
    public string Value { get; set; }

    /// <summary>
    ///     Access token id.
    /// </summary>
    public Guid AccessTokenId { get; set; }

    /// <summary>
    ///     Refresh token expired date.
    /// </summary>
    public DateTime ExpiredDate { get; set; }

    public DateTime CreatedAt { get; set; }

    public Guid CreatedBy { get; set; }

    // Navigation collections.
    public User User { get; set; }
}