using FuDever.Domain.Specifications.Base;
using FuDever.Domain.Specifications.Entities.UserRole;

namespace FuDever.SqlServer.Specifications.Entities.UserRole;

/// <summary>
///     Represent implementation of user
///     role as no tracking specification
/// </summary>
internal sealed class UserRoleAsNoTrackingSpecification :
    BaseSpecification<Domain.Entities.UserRole>,
    IUserRoleAsNoTrackingSpecification
{
    public UserRoleAsNoTrackingSpecification()
    {
        IsAsNoTracking = true;
    }
}
