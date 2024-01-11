using Domain.Specifications.Base;
using Domain.Specifications.Entities.RefreshToken;
using Microsoft.EntityFrameworkCore;
using Persistence.SqlServer2016.Common;

namespace Persistence.SqlServer2016.Specifications.Entities.RefreshToken;

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
                CustomConstant.DbCollation.SQL_LATIN1_GENERAL_CP1_CS_AS)
            .Equals(refreshTokenValue);
    }
}
