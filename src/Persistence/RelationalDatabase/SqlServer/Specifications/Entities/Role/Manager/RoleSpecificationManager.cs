using Domain.Specifications.Entities.Role;
using Domain.Specifications.Entities.Role.Manager;
using System;

namespace Persistence.RelationalDatabase.SqlServer.Specifications.Entities.Role.Manager;

/// <summary>
///     Represent implementation of role specification manager.
/// </summary>
internal sealed class RoleSpecificationManager : IRoleSpecificationManager
{
    // Backing fields.
    private IRoleAsNoTrackingSpecification _roleAsNoTrackingSpecification;
    private IRoleNotTemporarilyRemovedSpecification _roleNotTemporarilyRemovedSpecification;
    private IRoleTemporarilyRemovedSpecification _roleTemporarilyRemovedSpecification;
    private ISelectFieldsFromRoleSpecification _selectFieldsFromRoleSpecification;

    public IRoleAsNoTrackingSpecification RoleAsNoTrackingSpecification
    {
        get
        {
            _roleAsNoTrackingSpecification ??= new RoleAsNoTrackingSpecification();

            return _roleAsNoTrackingSpecification;
        }
    }

    public IRoleNotTemporarilyRemovedSpecification RoleNotTemporarilyRemovedSpecification
    {
        get
        {
            _roleNotTemporarilyRemovedSpecification ??= new RoleNotTemporarilyRemovedSpecification();

            return _roleNotTemporarilyRemovedSpecification;
        }
    }

    public IRoleTemporarilyRemovedSpecification RoleTemporarilyRemovedSpecification
    {
        get
        {
            _roleTemporarilyRemovedSpecification ??= new RoleTemporarilyRemovedSpecification();

            return _roleTemporarilyRemovedSpecification;
        }
    }

    public ISelectFieldsFromRoleSpecification SelectFieldsFromRoleSpecification
    {
        get
        {
            _selectFieldsFromRoleSpecification ??= new SelectFieldsFromRoleSpecification();

            return _selectFieldsFromRoleSpecification;
        }
    }

    public IRoleByIdSpecification RoleByIdSpecification(Guid roleId)
    {
        return new RoleByIdSpecification(roleId: roleId);
    }

    public IRoleByNameSpecification RoleByNameSpecification(
        string roleName,
        bool isCaseSensitive)
    {
        return new RoleByNameSpecification(
            roleName: roleName,
            isCaseSensitive: isCaseSensitive);
    }
}
