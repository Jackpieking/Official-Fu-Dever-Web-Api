using FuDever.Domain.Specifications.Base;
using FuDever.Domain.Specifications.Entities.UserPlatform;

namespace FuDever.PostgresSql.Specifications.Entities.UserPlatform;

/// <summary>
///     Represent implementation of update field of
///     user platform specification.
/// </summary>
internal sealed class UpdateFieldOfUserPlatformSpecification :
    BaseSpecification<Domain.Entities.UserPlatform>,
    IUpdateFieldOfUserPlatformSpecification
{
}
