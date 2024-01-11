using Domain.Specifications.Base;
using Domain.Specifications.Entities.UserJoiningStatus;

namespace Persistence.SqlServer2016.Specifications.Entities.UserJoiningStatus;

/// <summary>
///     Represent implementation of user joining status
///     as no tracking specification.
/// </summary>
internal sealed class UserJoiningStatusAsNoTrackingSpecification :
    BaseSpecification<Domain.Entities.UserJoiningStatus>,
    IUserJoiningStatusAsNoTrackingSpecification
{
    internal UserJoiningStatusAsNoTrackingSpecification()
    {
        IsAsNoTracking = true;
    }
}
