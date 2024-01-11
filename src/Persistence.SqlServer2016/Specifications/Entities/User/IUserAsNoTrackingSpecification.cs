using Domain.Specifications.Base;
using Domain.Specifications.Entities.User;

namespace Persistence.SqlServer2016.Specifications.Entities.User;

/// <summary>
///     Represent implementation of user as no tracking
///     specification.
/// </summary>
internal sealed class UserAsNoTrackingSpecification :
    BaseSpecification<Domain.Entities.User>,
    IUserAsNoTrackingSpecification
{
    internal UserAsNoTrackingSpecification()
    {
        IsAsNoTracking = true;
    }
}
