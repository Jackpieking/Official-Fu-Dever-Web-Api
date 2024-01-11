using System;
using Domain.Specifications.Base;
using Domain.Specifications.Entities.RefreshToken;

namespace Persistence.SqlServer2016.Specifications.Entities.RefreshToken;

/// <summary>
///     Represent implementation of expiration of refresh token specification.
/// </summary>
internal sealed class RefreshTokenExpiredSpecification :
    BaseSpecification<Domain.Entities.RefreshToken>,
    IRefreshTokenExpiredSpecification
{
    internal RefreshTokenExpiredSpecification()
    {
        var dateTimeNowInUtc = DateTime.UtcNow;

        WhereExpression = refreshToken => refreshToken.ExpiredDate < dateTimeNowInUtc;
    }
}
