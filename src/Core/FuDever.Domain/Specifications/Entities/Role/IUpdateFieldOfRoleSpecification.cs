using FuDever.Domain.Specifications.Base;
using System;

namespace FuDever.Domain.Specifications.Entities.Role;

/// <summary>
///     Represent update field of role specification.
/// </summary>
public interface IUpdateFieldOfRoleSpecification : IBaseSpecification<Domain.Entities.Role>
{
    IUpdateFieldOfRoleSpecification Ver1(
        DateTime roleRemovedAt,
        Guid roleRemovedBy);

    IUpdateFieldOfRoleSpecification Ver2(string roleName);
}
