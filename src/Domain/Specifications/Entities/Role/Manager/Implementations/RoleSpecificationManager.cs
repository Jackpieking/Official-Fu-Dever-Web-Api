using FuDeverWebApi.DataAccess.Specifications.Entites.Role.Manager.Contracts;
using System;

namespace FuDeverWebApi.DataAccess.Specifications.Entites.Role.Manager.Implementations;

public sealed class RoleSpecificationManager :
    IRoleSpecificationManager
{
    // Backing fields.
    private NoTrackingOnRoleSpecification _noTrackingOnRoleSpecification;
    private IsRoleNotSoftRemovedSpecification _isRoleNotSoftRemovedSpecification;
    private IsRoleSoftRemovedSpecification _isRoleSoftRemovedSpecification;
    private SelectFieldsFromRoleSpecification _selectFieldsFromRoleSpecification;
    private RoleByIdSpecification _roleByIdSpecification;
    private RoleByNameSpecification _roleByNameSpecification;

    public NoTrackingOnRoleSpecification NoTrackingOnRoleSpecification
    {
        get
        {
            _noTrackingOnRoleSpecification ??= new();

            return _noTrackingOnRoleSpecification;
        }
    }

    public IsRoleNotSoftRemovedSpecification IsRoleNotSoftRemovedSpecification
    {
        get
        {
            _isRoleNotSoftRemovedSpecification ??= new();

            return _isRoleNotSoftRemovedSpecification;
        }
    }

    public IsRoleSoftRemovedSpecification IsRoleSoftRemovedSpecification
    {
        get
        {
            _isRoleSoftRemovedSpecification ??= new();

            return _isRoleSoftRemovedSpecification;
        }
    }

    public SelectFieldsFromRoleSpecification SelectFieldsFromRoleSpecification
    {
        get
        {
            _selectFieldsFromRoleSpecification ??= new();

            return _selectFieldsFromRoleSpecification;
        }
    }

    public RoleByIdSpecification RoleByIdSpecification(Guid roleId)
    {
        return new(roleId: roleId);
    }

    public RoleByNameSpecification RoleByNameSpecification(
        string roleName,
        bool isCaseSensitive = false)
    {
        return new(
            roleName: roleName,
            isCaseSensitive: isCaseSensitive);
    }
}
