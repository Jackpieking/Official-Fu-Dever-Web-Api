using FuDever.Domain.Specifications.Base;
using FuDever.Domain.Specifications.Entities.UserHobby;

namespace FuDever.PostgresSql.Specifications.Entities.UserHobby;

/// <summary>
///     Represent implementation of update field of
///     user hobby specification.
/// </summary>
internal sealed class UpdateFieldOfUserHobbySpecification :
    BaseSpecification<Domain.Entities.UserHobby>,
    IUpdateFieldOfUserHobbySpecification
{
}
