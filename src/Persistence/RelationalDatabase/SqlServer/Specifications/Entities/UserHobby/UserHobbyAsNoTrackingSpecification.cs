using Domain.Specifications.Base;
using Domain.Specifications.Entities.UserHobby;

namespace Persistence.RelationalDatabase.SqlServer.Specifications.Entities.UserHobby;

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
