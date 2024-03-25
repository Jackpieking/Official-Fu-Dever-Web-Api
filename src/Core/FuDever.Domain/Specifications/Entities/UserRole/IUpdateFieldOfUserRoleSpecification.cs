using FuDever.Domain.Specifications.Base;
using System;

namespace FuDever.Domain.Specifications.Entities.UserRole;

/// <summary>
///     Represent update field of user role specification.
/// </summary>
public interface IUpdateFieldOfUserRoleSpecification : IBaseSpecification<Domain.Entities.UserRole>
{
    IUpdateFieldOfUserRoleSpecification Ver1(
        DateTime userUpdatedAt,
        Guid userUpdatedBy);
}
