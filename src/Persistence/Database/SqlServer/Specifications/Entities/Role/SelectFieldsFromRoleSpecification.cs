using Domain.Specifications.Base;
using Domain.Specifications.Entities.Role;

namespace Persistence.Database.SqlServer.Specifications.Entities.Role;

/// <summary>
///     Represent implementation of select fields from the "Roles"
///     table specification.
/// </summary>
internal sealed class SelectFieldsFromRoleSpecification :
    BaseSpecification<Domain.Entities.Role>,
    ISelectFieldsFromRoleSpecification
{
    public ISelectFieldsFromRoleSpecification Ver1()
    {
        SelectExpression = role => Domain.Entities.Role.Init(
            role.Id,
            role.Name);

        return this;
    }

    public ISelectFieldsFromRoleSpecification Ver2()
    {
        SelectExpression = role => Domain.Entities.Role.Init(
            role.Id,
            role.Name,
            role.RemovedAt,
            role.RemovedBy);

        return this;
    }
}
