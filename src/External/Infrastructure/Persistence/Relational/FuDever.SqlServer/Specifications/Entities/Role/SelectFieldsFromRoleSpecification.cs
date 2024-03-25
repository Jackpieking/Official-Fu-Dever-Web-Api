using FuDever.Domain.EntityBuilders.Role;
using FuDever.Domain.Specifications.Base;
using FuDever.Domain.Specifications.Entities.Role;

namespace FuDever.SqlServer.Specifications.Entities.Role;

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
        RoleForDatabaseRetrievingBuilder builder = new();

        SelectExpression = role => builder
            .WithId(role.Id)
            .WithName(role.Name)
            .Complete();

        return this;
    }

    public ISelectFieldsFromRoleSpecification Ver2()
    {
        RoleForDatabaseRetrievingBuilder builder = new();

        SelectExpression = role => builder
            .WithId(role.Id)
            .WithName(role.Name)
            .WithRemovedAt(role.RemovedAt)
            .WithRemovedBy(role.RemovedBy)
            .Complete();

        return this;
    }

    public ISelectFieldsFromRoleSpecification Ver3()
    {
        RoleForDatabaseRetrievingBuilder builder = new();

        SelectExpression = role => builder
            .WithName(role.Name)
            .Complete();

        return this;
    }
}
