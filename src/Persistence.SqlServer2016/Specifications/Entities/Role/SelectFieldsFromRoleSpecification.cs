using Domain.Specifications.Base;
using Domain.Specifications.Entities.Role;

namespace Persistence.SqlServer2016.Specifications.Entities.Role;

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
        SelectExpression = role => new()
        {
            Id = role.Id,
            Name = role.Name
        };

        return this;
    }

    public ISelectFieldsFromRoleSpecification Ver2()
    {
        SelectExpression = role => new()
        {
            Id = role.Id,
            Name = role.Name,
            RemovedBy = role.RemovedBy,
            RemovedAt = role.RemovedAt
        };

        return this;
    }
}
