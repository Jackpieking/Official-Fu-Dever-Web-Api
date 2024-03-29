using FuDever.Domain.Specifications.Base;

namespace FuDever.Domain.Specifications.Entities.Platform;

/// <summary>
///     Represent select fields from the "Platforms" table specification.
/// </summary>
public interface ISelectFieldsFromPlatformSpecification : IBaseSpecification<Domain.Entities.Platform>
{
    ISelectFieldsFromPlatformSpecification Ver1();

    ISelectFieldsFromPlatformSpecification Ver2();

    ISelectFieldsFromPlatformSpecification Ver3();
}
