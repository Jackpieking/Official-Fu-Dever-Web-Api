using Domain.Specifications.Base;

namespace Domain.Specifications.Entities.Role;

/// <summary>
///     Represent select fields from the "Roles" table specification.
/// </summary>
public interface ISelectFieldsFromRoleSpecification : IBaseSpecification<Domain.Entities.Role>
{
    ISelectFieldsFromRoleSpecification Ver1();

    ISelectFieldsFromRoleSpecification Ver2();
}
