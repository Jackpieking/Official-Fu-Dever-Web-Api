using FuDeverWebApi.DataAccess.Data.Entities;
using FuDeverWebApi.DataAccess.Specifications.Generic.Implementations;
using System;

namespace FuDeverWebApi.DataAccess.Specifications.Entites.RefreshToken;

public sealed class RefreshTokenByAccessTokenIdSpecification :
    GenericSpecification<RefreshTokenEntity>
{
    public RefreshTokenByAccessTokenIdSpecification(Guid accessTokenId)
    {
        Criteria = refreshToken => refreshToken.AccessTokenId == accessTokenId;
    }
}
