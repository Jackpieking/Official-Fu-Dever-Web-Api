using Domain.Specifications.Entities.RefreshToken;
using Domain.Specifications.Entities.RefreshToken.Manager;
using System;

namespace Persistence.Database.SqlServer.Specifications.Entities.RefreshToken.Manager;

/// <summary>
///     Represent implementation of refresh token specification manager.
/// </summary>
internal sealed class RefreshTokenSpecificationManager : IRefreshTokenSpecificationManager
{
    // Backing fields
    private ISelectFieldsFromRefreshTokenSpecification _selectFieldsFromRefreshTokenSpecification;
    private IRefreshTokenExpiredSpecification _refreshTokenExpiredSpecification;

    public ISelectFieldsFromRefreshTokenSpecification SelectFieldsFromRefreshTokenSpecification
    {
        get
        {
            _selectFieldsFromRefreshTokenSpecification ??= new SelectFieldsFromRefreshTokenSpecification();

            return _selectFieldsFromRefreshTokenSpecification;
        }
    }

    public IRefreshTokenExpiredSpecification RefreshTokenExpiredSpecification
    {
        get
        {
            _refreshTokenExpiredSpecification ??= new RefreshTokenExpiredSpecification();

            return _refreshTokenExpiredSpecification;
        }
    }

    public IRefreshTokenByAccessTokenIdSpecification RefreshTokenByAccessTokenIdSpecification(Guid accessTokenId)
    {
        return new RefreshTokenByAccessTokenIdSpecification(accessTokenId: accessTokenId);
    }

    public IRefreshTokenByValueSpecification RefreshTokenByValueSpecification(string refreshTokenValue)
    {
        return new RefreshTokenByValueSpecification(refreshTokenValue: refreshTokenValue);
    }
}
