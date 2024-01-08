using FuDeverWebApi.DataAccess.Data.Entities;
using FuDeverWebApi.DataAccess.Specifications.Generic.Implementations;
using System;

namespace FuDeverWebApi.DataAccess.Specifications.Entites.RefreshToken;

public sealed class IsRefreshTokenExpiredSpecification :
    GenericSpecification<RefreshTokenEntity>
{
    public IsRefreshTokenExpiredSpecification()
    {
        var datetimeNowInUtc = DateTime.UtcNow;

        Criteria = refreshToken => refreshToken.ExpiredDate < datetimeNowInUtc;
    }
}
