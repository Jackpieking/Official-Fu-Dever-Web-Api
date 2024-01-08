using FuDeverWebApi.DataAccess.Specifications.Entites.RefreshToken.Manager.Contracts;
using System;

namespace FuDeverWebApi.DataAccess.Specifications.Entites.RefreshToken.Manager.Implementations;

public sealed class RefreshTokenSpecificationManager :
    IRefreshTokenSpecificationManager
{
    private SelectFieldsFromRefreshTokenSpecification _selectFieldsFromRefreshTokenSpecification;
    private IsRefreshTokenExpiredSpecification _isRefreshTokenExpiredSpecification;

    public SelectFieldsFromRefreshTokenSpecification SelectFieldsFromRefreshTokenSpecification
    {
        get
        {
            _selectFieldsFromRefreshTokenSpecification ??= new();

            return _selectFieldsFromRefreshTokenSpecification;
        }
    }

    public IsRefreshTokenExpiredSpecification IsRefreshTokenExpiredSpecification
    {
        get
        {
            _isRefreshTokenExpiredSpecification ??= new();

            return _isRefreshTokenExpiredSpecification;
        }
    }

    public RefreshTokenByAccessTokenIdSpecification RefreshTokenByAccessTokenIdSpecification(
        Guid accessTokenId)
    {
        return new(accessTokenId: accessTokenId);
    }

    public RefreshTokenByRefreshTokenValueSpecification RefreshTokenByRefreshTokenValueSpecification(
        string refreshTokenValue)
    {
        return new(refreshTokenValue: refreshTokenValue);
    }
}
