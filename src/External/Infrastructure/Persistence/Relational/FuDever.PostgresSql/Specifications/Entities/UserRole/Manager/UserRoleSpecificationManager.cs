using FuDever.Domain.Specifications.Entities.UserRole;
using FuDever.Domain.Specifications.Entities.UserRole.Manager;
using System;

namespace FuDever.PostgresSql.Specifications.Entities.UserRole.Manager;

/// <summary>
///     Represent implementation of user role specification manager.
/// </summary>
internal sealed class UserRoleSpecificationManager : IUserRoleSpecificationManager
{
    // Backing fields.
    private IUserRoleAsNoTrackingSpecification _userRoleAsNoTrackingSpecification;
    private ISelectFieldsFromUserRoleSpecification _selectFieldsFromUserRoleSpecification;
    private IUpdateFieldOfUserRoleSpecification _updateFieldOfUserRoleSpecification;

    public IUserRoleByRoleIdSpecification UserRoleByRoleIdSpecification(Guid roleId)
    {
        return new UserRoleByRoleIdSpecification(roleId: roleId);
    }

    public ISelectFieldsFromUserRoleSpecification SelectFieldsFromUserRoleSpecification
    {
        get
        {
            _selectFieldsFromUserRoleSpecification ??= new SelectFieldsFromUserRoleSpecification();

            return _selectFieldsFromUserRoleSpecification;
        }
    }

    public IUserRoleAsNoTrackingSpecification UserRoleAsNoTrackingSpecification
    {
        get
        {
            _userRoleAsNoTrackingSpecification ??= new UserRoleAsNoTrackingSpecification();

            return _userRoleAsNoTrackingSpecification;
        }
    }

    public IUpdateFieldOfUserRoleSpecification UpdateFieldOfUserRoleSpecification
    {
        get
        {
            _updateFieldOfUserRoleSpecification ??= new UpdateFieldOfUserRoleSpecification();

            return _updateFieldOfUserRoleSpecification;
        }
    }
}
