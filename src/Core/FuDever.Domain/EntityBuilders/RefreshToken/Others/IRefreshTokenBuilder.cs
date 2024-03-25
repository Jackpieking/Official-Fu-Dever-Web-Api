using FuDever.Domain.EntityBuilders.Others;
using System;

namespace FuDever.Domain.EntityBuilders.RefreshToken.Others;

/// <summary>
///     Interface for refresh token builder.
/// </summary>
public interface IRefreshTokenBuilder<TBuilder> :
    IBaseEntityHandler<Entities.RefreshToken>
        where TBuilder : IBaseRefreshTokenBuilder
{
    /// <summary>
    ///     Set the refresh token id.
    /// </summary>
    /// <param name="refreshTokenId">
    ///     Refresh token id.
    /// </param>
    /// <returns>
    ///     Itself.
    /// </returns>
    TBuilder WithId(Guid refreshTokenId);

    /// <summary>
    ///     Set the refresh token value.
    /// </summary>
    /// <param name="refreshTokenValue">
    ///     Refresh token value.
    /// </param>
    /// <returns>
    ///     Itself.
    /// </returns>
    TBuilder WithValue(string refreshTokenValue);

    /// <summary>
    ///     Set the access token id.
    /// </summary>
    /// <param name="accessTokenId">
    ///     Access token id.
    /// </param>
    /// <returns>
    ///     Itself.
    /// </returns>
    TBuilder WithAccessTokenId(Guid accessTokenId);

    /// <summary>
    ///     Set the refresh token expired date.
    /// </summary>
    /// <param name="refreshTokenExpiredDate">
    ///     Refresh token expired date.
    /// </param>
    /// <returns>
    ///     Itself.
    /// </returns>
    TBuilder WithExpiredDate(DateTime refreshTokenExpiredDate);

    /// <summary>
    ///     Set the refresh token created date.
    /// </summary>
    /// <param name="refreshTokenCreatedAt">
    ///     Refresh token created date.
    /// </param>
    /// <returns>
    ///     Itself.
    /// </returns>
    TBuilder WithCreatedAt(DateTime refreshTokenCreatedAt);

    /// <summary>
    ///     Set the refresh token created by.
    /// </summary>
    /// <param name="refreshTokenCreatedBy">
    ///     Refresh token created by.
    /// </param>
    /// <returns>
    ///     Itself.
    /// </returns>
    TBuilder WithCreatedBy(Guid refreshTokenCreatedBy);
}
