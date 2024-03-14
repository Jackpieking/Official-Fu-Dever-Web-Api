using System;

namespace FuDever.Domain.Specifications.Entities.RefreshToken.Manager;

/// <summary>
///     Represent refresh token specification manager.
/// </summary>
public interface IRefreshTokenSpecificationManager
{
    /// <summary>
    ///     Refresh token by access token id specification.
    /// </summary>
    /// <param name="accessTokenId">
    ///     Access token id for finding refresh token.
    /// </param>
    /// <returns>
    ///     Specification.
    /// </returns>
    IRefreshTokenByAccessTokenIdSpecification RefreshTokenByAccessTokenIdSpecification(Guid accessTokenId);

    /// <summary>
    ///     Refresh token by refresh token value specification.
    /// </summary>
    /// <param name="refreshTokenValue">
    ///     Refresh token value for finding refresh token.
    /// </param>
    /// <returns>
    ///     Specification.
    /// </returns>
    IRefreshTokenByValueSpecification RefreshTokenByValueSpecification(string refreshTokenValue);

    /// <summary>
    ///     Select field from "RefreshTokens" table specification.
    /// </summary>
    ISelectFieldsFromRefreshTokenSpecification SelectFieldsFromRefreshTokenSpecification { get; }

    /// <summary>
    ///     Expiration of refresh token specification.
    /// </summary>
    IRefreshTokenExpiredSpecification RefreshTokenExpiredSpecification { get; }
}
