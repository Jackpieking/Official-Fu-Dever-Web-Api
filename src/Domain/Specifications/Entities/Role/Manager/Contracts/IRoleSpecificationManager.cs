using System;

namespace FuDeverWebApi.DataAccess.Specifications.Entites.Role.Manager.Contracts;

public interface IRoleSpecificationManager
{
    NoTrackingOnRoleSpecification NoTrackingOnRoleSpecification { get; }

    IsRoleNotSoftRemovedSpecification IsRoleNotSoftRemovedSpecification { get; }

    IsRoleSoftRemovedSpecification IsRoleSoftRemovedSpecification { get; }

    RoleByIdSpecification RoleByIdSpecification(Guid roleId);

    RoleByNameSpecification RoleByNameSpecification(
        string roleName,
        bool isCaseSensitive = false);

    SelectFieldsFromRoleSpecification SelectFieldsFromRoleSpecification { get; }
}
