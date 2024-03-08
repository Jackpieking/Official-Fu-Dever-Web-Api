using Domain.Entities.Base;
using Microsoft.IdentityModel.JsonWebTokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;

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
    public static RefreshToken InitVer1(Guid refreshTokenId)
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
    /// <param name="createdBy">
    ///     Id of refresh token creator.
    /// </param>
    /// <returns>
    ///     A new refresh token object.
    /// </returns>
    public static RefreshToken InitVer2(Guid createdBy)
    {
        // Validate refresh token Id.
        if (createdBy == Guid.Empty)
        {
            return default;
        }

        return new()
        {
            CreatedBy = createdBy
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
    public static RefreshToken InitVer3(string refreshTokenValue)
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

    /// <summary>
    ///     Return an instance.
    /// </summary>
    /// <param name="claims">
    ///     List of user claims.
    /// </param>
    /// <param name="rememberMe">
    ///     Do user want to remember him/herself.
    /// </param>
    /// <returns>
    ///     A new refresh token object.
    /// </returns>
    public static RefreshToken InitVer4(
        IEnumerable<Claim> claims,
        string value,
        bool rememberMe,
        DateTime createdAt)
    {
        // Validate claim list.
        if (Equals(objA: claims, objB: null) ||
            claims.Equals(obj: Enumerable.Empty<Claim>()))
        {
            return default;
        }

        // Validate refresh token value.
        if (string.IsNullOrWhiteSpace(value: value))
        {
            return default;
        }

        return new()
        {
            Id = Guid.NewGuid(),
            CreatedBy = Guid.Parse(input: claims
                .First(predicate: claim => claim.Type.Equals(
                    value: JwtRegisteredClaimNames.Sub))
                .Value),
            Value = value,
            AccessTokenId = new(g: claims
                .First(predicate: claim => claim.Type.Equals(
                    value: JwtRegisteredClaimNames.Jti))
                .Value),
            ExpiredDate = rememberMe ?
                DateTime.UtcNow.AddDays(value: 7) :
                DateTime.UtcNow.AddDays(value: 3),
            CreatedAt = createdAt
        };
    }

    /// <summary>
    ///     Represent metadata of property.
    /// </summary>
    public static class Metadata
    {
        /// <summary>
        ///     Value property.
        /// </summary>
        public static class Value
        {
            /// <summary>
            ///     Max value length.
            /// </summary>
            public const int MaxLength = 100;

            /// <summary>
            ///     Min value length.
            /// </summary>
            public const int MinLength = 2;
        }
    }
}