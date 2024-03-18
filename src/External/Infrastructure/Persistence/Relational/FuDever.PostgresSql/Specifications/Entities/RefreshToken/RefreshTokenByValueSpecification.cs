using FuDever.Domain.Specifications.Base;
using FuDever.Domain.Specifications.Entities.RefreshToken;

namespace FuDever.PostgresSql.Specifications.Entities.RefreshToken;

/// <summary>
///     Represent implementation of refresh token
///     by refresh token value specification.
/// </summary>
internal sealed class RefreshTokenByValueSpecification :
    BaseSpecification<Domain.Entities.RefreshToken>,
    IRefreshTokenByValueSpecification
{
    internal RefreshTokenByValueSpecification(string refreshTokenValue)
    {
        WhereExpression = refreshToken => refreshToken.Value.Equals(refreshTokenValue);
    }
}
