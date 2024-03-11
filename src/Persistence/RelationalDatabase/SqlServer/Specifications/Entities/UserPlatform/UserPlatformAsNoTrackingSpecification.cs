using Domain.Specifications.Base;
using Domain.Specifications.Entities.UserPlatform;

namespace Persistence.RelationalDatabase.SqlServer.Specifications.Entities.UserPlatform;

/// <summary>
///     Represent implementation of user
///     platform as no tracking specification
/// </summary>
internal sealed class UserPlatformAsNoTrackingSpecification :
    BaseSpecification<Domain.Entities.UserPlatform>,
    IUserPlatformAsNoTrackingSpecification
{
    public UserPlatformAsNoTrackingSpecification()
    {
        IsAsNoTracking = true;
    }
}
