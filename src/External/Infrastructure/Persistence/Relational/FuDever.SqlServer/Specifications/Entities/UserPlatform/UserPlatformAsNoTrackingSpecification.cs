using FuDever.Domain.Specifications.Base;
using FuDever.Domain.Specifications.Entities.UserPlatform;

namespace FuDever.SqlServer.Specifications.Entities.UserPlatform;

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
