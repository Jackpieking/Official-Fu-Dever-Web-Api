using FuDever.Domain.EntityBuilders.UserRole;
using FuDever.Domain.Specifications.Base;
using FuDever.Domain.Specifications.Entities.UserRole;

namespace FuDever.SqlServer.Specifications.Entities.UserRole;

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
        UserRoleForDatabaseRetrievingBuilder builder = new();

        SelectExpression = userRole => builder
            .WithUserId(userRole.UserId)
            .Complete();

        return this;
    }
}
