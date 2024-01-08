using System;

namespace Domain.Specifications.Entities.Role.Manager;

/// <summary>
///     Represent role specification manager.
/// </summary>
public interface IRoleSpecificationManager
{
    /// <summary>
    ///     Role as no tracking specification.
    /// </summary>
    IRoleAsNoTrackingSpecification RoleAsNoTrackingSpecification { get; }

    /// <summary>
    ///     Role not temporarily removed specification.
    /// </summary>
    IRoleNotTemporarilyRemovedSpecification RoleNotTemporarilyRemovedSpecification { get; }

    /// <summary>
    ///     Role temporarily removed specification.
    /// </summary>
    IRoleTemporarilyRemovedSpecification RoleTemporarilyRemovedSpecification { get; }

    /// <summary>
    ///     Role by role id specification.
    /// </summary>
    /// <param name="positionId">
    ///     Role id for finding role.
    /// </param>
    /// <returns>
    ///     Specification.
    /// </returns>
    IRoleByIdSpecification RoleByIdSpecification(Guid roleId);

    /// <summary>
    ///     Role by role name specification.
    /// </summary>
    /// <param name="roleName">
    ///     Role name for finding position.
    /// </param>
    /// <param name="isCaseSensitive">
    ///     Does role name need searching in a sensitive way.
    /// </param>
    /// <returns>
    ///     Specification.
    /// </returns>
    IRoleByNameSpecification RoleByNameSpecification(
        string roleName,
        bool isCaseSensitive);

    /// <summary>
    ///     Select field from "Roles" table specification.
    /// </summary>
    ISelectFieldsFromRoleSpecification SelectFieldsFromRoleSpecification { get; }
}
