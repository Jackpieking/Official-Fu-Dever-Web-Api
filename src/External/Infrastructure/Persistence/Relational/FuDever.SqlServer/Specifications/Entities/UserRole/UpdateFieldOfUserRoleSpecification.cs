using FuDever.Domain.Specifications.Base;
using FuDever.Domain.Specifications.Entities.UserRole;

namespace FuDever.SqlServer.Specifications.Entities.UserRole;

/// <summary>
///     Represent implementation of update
///     field of user role specification.
/// </summary>
internal sealed class UpdateFieldOfUserRoleSpecification :
    BaseSpecification<Domain.Entities.UserRole>,
    IUpdateFieldOfUserRoleSpecification
{
}
