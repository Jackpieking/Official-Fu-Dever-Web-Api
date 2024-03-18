using FuDever.Domain.Specifications.Base;
using FuDever.Domain.Specifications.Entities.Role;

namespace FuDever.PostgresSql.Specifications.Entities.Role;

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
        SelectExpression = role => Domain.Entities.Role.InitFromDatabaseVer1(
            role.Id,
            role.Name);

        return this;
    }

    public ISelectFieldsFromRoleSpecification Ver2()
    {
        SelectExpression = role => Domain.Entities.Role.InitFromDatabaseVer3(
            role.Id,
            role.Name,
            role.RemovedAt,
            role.RemovedBy);

        return this;
    }

    public ISelectFieldsFromRoleSpecification Ver3()
    {
        SelectExpression = role => Domain.Entities.Role.InitFromDatabaseVer2(role.Name);

        return this;
    }
}
