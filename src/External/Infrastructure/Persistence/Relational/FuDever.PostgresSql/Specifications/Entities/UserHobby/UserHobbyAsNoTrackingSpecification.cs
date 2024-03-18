using FuDever.Domain.Specifications.Base;
using FuDever.Domain.Specifications.Entities.UserHobby;

namespace FuDever.PostgresSql.Specifications.Entities.UserHobby;

/// <summary>
///     Represent implementation of user
///     hobby as no tracking specification.
/// </summary>
internal sealed class UserHobbyAsNoTrackingSpecification :
    BaseSpecification<Domain.Entities.UserHobby>,
    IUserHobbyAsNoTrackingSpecification
{
    public UserHobbyAsNoTrackingSpecification()
    {
        IsAsNoTracking = true;
    }
}
