using System;

namespace FuDeverWebApi.DataAccess.Specifications.Entites.RefreshToken.Manager.Contracts;

public interface IRefreshTokenSpecificationManager
{
    RefreshTokenByAccessTokenIdSpecification RefreshTokenByAccessTokenIdSpecification(Guid accessTokenId);

    RefreshTokenByRefreshTokenValueSpecification RefreshTokenByRefreshTokenValueSpecification(string refreshTokenValue);

    SelectFieldsFromRefreshTokenSpecification SelectFieldsFromRefreshTokenSpecification { get; }

    IsRefreshTokenExpiredSpecification IsRefreshTokenExpiredSpecification { get; }
}
