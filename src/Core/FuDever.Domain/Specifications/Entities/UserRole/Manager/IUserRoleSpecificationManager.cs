using System;

namespace FuDever.Domain.Specifications.Entities.UserRole.Manager;

/// <summary>
///     Represent user role specification manager.
/// </summary>
public interface IUserRoleSpecificationManager
{
    /// <summary>
    ///     User role by role id specification.
    /// </summary>
    /// <param name="roleId">
    ///     role id for finding user role.
    /// </param>
    /// <returns>
    ///     Specification.
    /// </returns>
    IUserRoleByRoleIdSpecification UserRoleByRoleIdSpecification(Guid roleId);


    /// <summary>
    ///     Select field from "UserRoles" table specification.
    /// </summary>
    ISelectFieldsFromUserRoleSpecification SelectFieldsFromUserRoleSpecification { get; }

    /// <summary>
    ///     User role as no tracking specification.
    /// </summary>
    IUserRoleAsNoTrackingSpecification UserRoleAsNoTrackingSpecification { get; }

    /// <summary>
    ///     Update field of user role specification.
    /// </summary>
    IUpdateFieldOfUserRoleSpecification UpdateFieldOfUserRoleSpecification { get; }
}
