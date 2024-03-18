using FuDever.Domain.Entities.Base;
using Microsoft.IdentityModel.JsonWebTokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;

namespace FuDever.Domain.Entities;

/// <summary>
///     Represent the "RefreshTokens" table.
/// </summary>
public sealed class RefreshToken :
    IBaseEntity,
    ICreatedEntity
{
    private RefreshToken() { }

    public Guid Id { get; set; }

    public string Value { get; set; }

    public Guid AccessTokenId { get; set; }

    public DateTime ExpiredDate { get; set; }

    public DateTime CreatedAt { get; set; }

    public Guid CreatedBy { get; set; }

    // Navigation collections.
    public User User { get; set; }

    public static RefreshToken InitFromDatabaseVer1(Guid refreshTokenId)
    {
        return new()
        {
            Id = refreshTokenId
        };
    }

    public static RefreshToken InitFromDatabaseVer2(Guid refreshTokenCreatedBy)
    {
        return new()
        {
            CreatedBy = refreshTokenCreatedBy
        };
    }

    /// <summary>
    ///     Return an instance.
    /// </summary>
    /// <param name="refreshTokenClaims">
    ///     List of user claims.
    /// </param>
    /// <param name="refreshTokenValue">
    ///     Refresh token value.
    /// </param>
    /// <param name="refreshTokenRememberMe">
    ///     Do user want to remember him/herself.
    /// </param>
    /// <param name="refreshTokenCreatedAt">
    ///     When is refresh token created.
    /// </param>
    /// <returns>
    ///     A new refresh token object.
    /// </returns>
    public static RefreshToken InitVer1(
        IEnumerable<Claim> refreshTokenClaims,
        string refreshTokenValue,
        bool refreshTokenRememberMe,
        DateTime refreshTokenCreatedAt)
    {
        // Validate claim list.
        if (Equals(objA: refreshTokenClaims, objB: null) ||
            refreshTokenClaims.Equals(obj: Enumerable.Empty<Claim>()))
        {
            return default;
        }

        // Validate refresh token value.
        if (string.IsNullOrWhiteSpace(value: refreshTokenValue) ||
            refreshTokenValue.Length > Metadata.Value.MaxLength ||
            refreshTokenValue.Length < Metadata.Value.MinLength)
        {
            return default;
        }

        return new()
        {
            Id = Guid.NewGuid(),
            CreatedBy = Guid.Parse(input: refreshTokenClaims
                .First(predicate: claim => claim.Type.Equals(
                    value: JwtRegisteredClaimNames.Sub))
                .Value),
            Value = refreshTokenValue,
            AccessTokenId = new(g: refreshTokenClaims
                .First(predicate: claim => claim.Type.Equals(
                    value: JwtRegisteredClaimNames.Jti))
                .Value),
            ExpiredDate = refreshTokenRememberMe ?
                DateTime.UtcNow.AddDays(value: 7) :
                DateTime.UtcNow.AddDays(value: 3),
            CreatedAt = refreshTokenCreatedAt
        };
    }

    public static class Metadata
    {
        public static class Value
        {
            public const int MaxLength = 100;

            public const int MinLength = 2;
        }
    }
}