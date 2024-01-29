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

    /// <summary>
    ///     Return an instance.
    /// </summary>
    /// <param name="claims">
    ///     List of user claims.
    /// </param>
    /// <param name="rememberMe">
    ///     Do user want to remember him/herself.
    /// </param>
    /// <param name="length">
    ///     Length of refresh token.
    /// </param>
    /// <returns>
    ///     A new refresh token object.
    /// </returns>
    public static RefreshToken Init(
        IEnumerable<Claim> claims,
        bool rememberMe,
        int length)
    {
        // Validate claim list.
        if (Equals(objA: claims, objB: null) ||
            claims.Equals(obj: Enumerable.Empty<Claim>()))
        {
            return default;
        }

        // Validate refresh token length.
        if (length == default)
        {
            return default;
        }

        return new()
        {
            Id = Guid.NewGuid(),
            UserId = Guid.Parse(input: claims
                .First(predicate: claim => claim.Type.Equals(value: JwtRegisteredClaimNames.Sub))
                .Value),
            Value = RandomStringGenerator(length: length),
            AccessTokenId = new(g: claims
                .First(predicate: claim => claim.Type.Equals(value: JwtRegisteredClaimNames.Jti))
                .Value),
            ExpiredDate = rememberMe ?
                DateTime.UtcNow.AddDays(value: 7) :
                DateTime.UtcNow.AddDays(value: 3)
        };
    }

    /// <summary>
    ///     Generate the value for the refresh token.
    /// </summary>
    /// <param name="length">
    ///     Length of the value.
    /// </param>
    /// <returns>
    ///     A random string with given length.
    /// </returns>
    private static string RandomStringGenerator(int length)
    {
        const string Chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890abcdefghijklmnopqrstuvwxyz_!@#$%^&*()_-+=";

        Random random = new();

        return new(
            value: Enumerable
                .Repeat(element: Chars, count: length)
                .Select(selector: s => s[random.Next(maxValue: s.Length)])
                .ToArray()
        );
    }
}