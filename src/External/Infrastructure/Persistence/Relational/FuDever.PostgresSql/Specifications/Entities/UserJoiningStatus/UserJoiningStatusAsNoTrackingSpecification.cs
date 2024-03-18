using FuDever.Domain.Specifications.Base;
using FuDever.Domain.Specifications.Entities.UserJoiningStatus;

namespace FuDever.PostgresSql.Specifications.Entities.UserJoiningStatus;

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
