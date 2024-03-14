using FuDever.Domain.Specifications.Base;
using FuDever.Domain.Specifications.Entities.RefreshToken;

namespace FuDever.SqlServer.Specifications.Entities.RefreshToken;

/// <summary>
///     Represent implementation of select fields from the "RefreshTokens"
///     table specification.
/// </summary>
internal sealed class SelectFieldsFromRefreshTokenSpecification :
    BaseSpecification<Domain.Entities.RefreshToken>,
    ISelectFieldsFromRefreshTokenSpecification
{
    public ISelectFieldsFromRefreshTokenSpecification Ver1()
    {
        SelectExpression = refreshToken => Domain.Entities.RefreshToken.InitVer1(refreshToken.Id);

        return this;
    }

    public ISelectFieldsFromRefreshTokenSpecification Ver2()
    {
        SelectExpression = refreshToken => Domain.Entities.RefreshToken.InitVer2(refreshToken.CreatedBy);

        return this;
    }
}