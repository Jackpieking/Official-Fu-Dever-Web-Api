using FuDeverWebApi.DataAccess.Data.Entities;
using FuDeverWebApi.DataAccess.Specifications.Generic.Implementations;

namespace FuDeverWebApi.DataAccess.Specifications.Entites.RefreshToken;

public sealed class SelectFieldsFromRefreshTokenSpecification :
    GenericSpecification<RefreshTokenEntity>
{
    public SelectFieldsFromRefreshTokenSpecification Ver1()
    {
        SelectExpression = refreshToken => new()
        {
            Id = refreshToken.Id
        };

        return this;
    }

    public SelectFieldsFromRefreshTokenSpecification Ver2()
    {
        SelectExpression = refreshToken => new()
        {
            UserId = refreshToken.UserId
        };

        return this;
    }
}
