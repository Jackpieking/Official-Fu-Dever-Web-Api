using FuDever.Domain.Specifications.Base;

namespace FuDever.Domain.Specifications.Entities.UserRole;

/// <summary>
///     Represent select fields from the "UserRoles" table specification.
/// </summary>
public interface ISelectFieldsFromUserRoleSpecification : IBaseSpecification<Domain.Entities.UserRole>
{
    ISelectFieldsFromUserRoleSpecification Ver1();
}
