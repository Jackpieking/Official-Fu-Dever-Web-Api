using FuDeverWebApi.DataAccess.Data.Entities;
using FuDeverWebApi.DataAccess.Specifications.Generic.Implementations;
using FuDeverWebApi.Helpers;
using Microsoft.EntityFrameworkCore;

namespace FuDeverWebApi.DataAccess.Specifications.Entites.RefreshToken;

public sealed class RefreshTokenByRefreshTokenValueSpecification :
    GenericSpecification<RefreshTokenEntity>
{
    public RefreshTokenByRefreshTokenValueSpecification(string refreshTokenValue)
    {
        Criteria = refreshToken => EF.Functions
            .Collate(
                refreshToken.Value,
                CustomConstants.SqlCollation.SQL_LATIN1_GENERAL_CP1_CS_AS)
            .Equals(refreshTokenValue);
    }
}
