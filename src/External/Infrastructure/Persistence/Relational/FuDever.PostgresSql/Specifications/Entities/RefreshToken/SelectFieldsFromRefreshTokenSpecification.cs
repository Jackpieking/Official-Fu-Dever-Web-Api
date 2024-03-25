using FuDever.Domain.EntityBuilders.RefreshToken;
using FuDever.Domain.Specifications.Base;
using FuDever.Domain.Specifications.Entities.RefreshToken;

namespace FuDever.PostgresSql.Specifications.Entities.RefreshToken;

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
        RefreshTokenForDatabaseRetrievingBuilder builder = new();

        SelectExpression = refreshToken => builder
            .WithId(refreshToken.Id)
            .Complete();

        return this;
    }

    public ISelectFieldsFromRefreshTokenSpecification Ver2()
    {
        RefreshTokenForDatabaseRetrievingBuilder builder = new();

        SelectExpression = refreshToken => builder
            .WithCreatedBy(refreshToken.CreatedBy)
            .Complete();

        return this;
    }
}
