using FuDever.Domain.Specifications.Base;

namespace FuDever.Domain.Specifications.Entities.UserPlatform;

/// <summary>
///     Represent select fields from the "UserPlatforms" table specification.
/// </summary>
public interface ISelectFieldsFromUserPlatformSpecification : IBaseSpecification<Domain.Entities.UserPlatform>
{
    ISelectFieldsFromUserPlatformSpecification Ver1();
}
