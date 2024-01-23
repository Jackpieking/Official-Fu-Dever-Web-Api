using Domain.Specifications.Base;
using Domain.Specifications.Entities.RefreshToken;
using System;

namespace Persistence.Database.SqlServer.Specifications.Entities.RefreshToken;

/// <summary>
///     Represent implementation of refresh token
///     by access token id specification.
/// </summary>
internal sealed class RefreshTokenByAccessTokenIdSpecification :
    BaseSpecification<Domain.Entities.RefreshToken>,
    IRefreshTokenByAccessTokenIdSpecification
{
    internal RefreshTokenByAccessTokenIdSpecification(Guid accessTokenId)
    {
        WhereExpression = refreshToken => refreshToken.AccessTokenId == accessTokenId;
    }
}
