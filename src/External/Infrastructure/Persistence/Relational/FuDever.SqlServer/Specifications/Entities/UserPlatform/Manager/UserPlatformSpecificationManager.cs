using FuDever.Domain.Specifications.Entities.UserPlatform;
using FuDever.Domain.Specifications.Entities.UserPlatform.Manager;
using System;

namespace FuDever.SqlServer.Specifications.Entities.UserPlatform.Manager;

/// <summary>
///     Represent implementation of user platform specification manager.
/// </summary>
internal sealed class UserPlatformSpecificationManager : IUserPlatformSpecificationManager
{
    // Backing fields.
    private ISelectFieldsFromUserPlatformSpecification _selectFieldsFromUserPlatformSpecification;
    private IUserPlatformAsNoTrackingSpecification _userPlatformAsNoTrackingSpecification;
    private IUpdateFieldOfUserPlatformSpecification _updateFieldOfUserPlatformSpecification;

    public ISelectFieldsFromUserPlatformSpecification SelectFieldsFromUserPlatformSpecification
    {
        get
        {
            _selectFieldsFromUserPlatformSpecification ??= new SelectFieldsFromUserPlatformSpecification();

            return _selectFieldsFromUserPlatformSpecification;
        }
    }

    public IUserPlatformAsNoTrackingSpecification UserPlatformAsNoTrackingSpecification
    {
        get
        {
            _userPlatformAsNoTrackingSpecification ??= new UserPlatformAsNoTrackingSpecification();

            return _userPlatformAsNoTrackingSpecification;
        }
    }

    public IUpdateFieldOfUserPlatformSpecification UpdateFieldOfUserPlatformSpecification
    {
        get
        {
            _updateFieldOfUserPlatformSpecification ??= new UpdateFieldOfUserPlatformSpecification();

            return _updateFieldOfUserPlatformSpecification;
        }
    }

    public IUserPlatformByPlatformIdSpecification UserPlatformByPlatformIdSpecification(Guid platformId)
    {
        return new UserPlatformByPlatformIdSpecification(platformId: platformId);
    }

    public IUserPlatformByUserIdSpecification UserPlatformByUserIdSpecification(Guid userId)
    {
        return new UserPlatformByUserIdSpecification(userId: userId);
    }
}
