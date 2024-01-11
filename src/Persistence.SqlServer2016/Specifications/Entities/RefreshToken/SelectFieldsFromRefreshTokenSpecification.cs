using Domain.Specifications.Base;
using Domain.Specifications.Entities.RefreshToken;

namespace Persistence.SqlServer2016.Specifications.Entities.RefreshToken;

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
        SelectExpression = refreshToken => new()
        {
            Id = refreshToken.Id
        };

        return this;
    }

    public ISelectFieldsFromRefreshTokenSpecification Ver2()
    {
        SelectExpression = refreshToken => new()
        {
            UserId = refreshToken.UserId
        };

        return this;
    }
}
