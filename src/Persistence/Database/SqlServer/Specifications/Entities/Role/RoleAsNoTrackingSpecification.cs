using Domain.Specifications.Base;
using Domain.Specifications.Entities.Role;

namespace Persistence.Database.SqlServer.Specifications.Entities.Role;

/// <summary>
///     Represent implementation of role as no
///     tracking specification.
/// </summary>
internal sealed class RoleAsNoTrackingSpecification :
    BaseSpecification<Domain.Entities.Role>,
    IRoleAsNoTrackingSpecification
{
    internal RoleAsNoTrackingSpecification()
    {
        IsAsNoTracking = true;
    }
}
