using FuDever.Domain.Specifications.Base;
using FuDever.Domain.Specifications.Entities.RefreshToken;
using FuDever.SqlServer.Commons;
using Microsoft.EntityFrameworkCore;

namespace FuDever.SqlServer.Specifications.Entities.RefreshToken;

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
        WhereExpression = refreshToken => EF.Functions
            .Collate(
                refreshToken.Value,
                CommonConstant.DbCollation.SQL_LATIN1_GENERAL_CP1_CS_AS)
            .Equals(refreshTokenValue);
    }
}
