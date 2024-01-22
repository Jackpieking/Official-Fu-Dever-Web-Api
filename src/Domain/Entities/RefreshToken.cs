using Domain.Entities.Base;
using System;

namespace Domain.Entities;

/// <summary>
///     Represent the "RefreshTokens" table.
/// </summary>
public sealed class RefreshToken :
    IBaseEntity,
    ICreatedEntity
{
    private RefreshToken() { }

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

    /// <summary>
    ///     Return an instance.
    /// </summary>
    /// <param name="refreshTokenId">
    ///     Id of refresh token.
    /// </param>
    /// <returns>
    ///     A new refresh token object.
    /// </returns>
    public static RefreshToken Init(Guid refreshTokenId)
    {
        // Validate refresh token Id.
        if (refreshTokenId == Guid.Empty)
        {
            return default;
        }

        return new()
        {
            Id = refreshTokenId
        };
    }

    /// <summary>
    ///     Return an instance.
    /// </summary>
    /// <param name="refreshTokenValue">
    ///     Refresh token value.
    /// </param>
    /// <returns>
    ///     A new refresh token object.
    /// </returns>
    public static RefreshToken Init(string refreshTokenValue)
    {
        // Validate refresh token value.
        if (string.IsNullOrWhiteSpace(value: refreshTokenValue))
        {
            return default;
        }

        return new()
        {
            Value = refreshTokenValue
        };
    }
}