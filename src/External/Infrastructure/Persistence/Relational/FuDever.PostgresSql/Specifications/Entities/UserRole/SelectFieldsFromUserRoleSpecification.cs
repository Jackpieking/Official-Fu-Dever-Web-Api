using FuDever.Domain.Specifications.Base;
using FuDever.Domain.Specifications.Entities.UserRole;

namespace FuDever.PostgresSql.Specifications.Entities.UserRole;

/// <summary>
///     Represent implementation of select fields from the "UserRoles"
///     table specification.
/// </summary>
internal sealed class SelectFieldsFromUserRoleSpecification :
    BaseSpecification<Domain.Entities.UserRole>,
    ISelectFieldsFromUserRoleSpecification
{
    public ISelectFieldsFromUserRoleSpecification Ver1()
    {
        SelectExpression = userRole => Domain.Entities.UserRole.InitFromDatabaseVer1(
            userRole.UserId);

        return this;
    }
}
