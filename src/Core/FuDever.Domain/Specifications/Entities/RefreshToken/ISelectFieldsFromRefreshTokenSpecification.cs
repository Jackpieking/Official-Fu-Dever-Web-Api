using FuDever.Domain.Specifications.Base;

namespace FuDever.Domain.Specifications.Entities.RefreshToken;

/// <summary>
///     Represent select fields from the "RefreshTokens" table specification.
/// </summary>
public interface ISelectFieldsFromRefreshTokenSpecification : IBaseSpecification<Domain.Entities.RefreshToken>
{
    ISelectFieldsFromRefreshTokenSpecification Ver1();

    ISelectFieldsFromRefreshTokenSpecification Ver2();
}
