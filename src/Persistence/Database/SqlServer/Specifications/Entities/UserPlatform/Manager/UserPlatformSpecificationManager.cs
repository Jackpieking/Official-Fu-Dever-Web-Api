using Domain.Specifications.Entities.UserPlatform;
using Domain.Specifications.Entities.UserPlatform.Manager;
using System;

namespace Persistence.Database.SqlServer.Specifications.Entities.UserPlatform.Manager;

/// <summary>
///     Represent implementation of user platform specification manager.
/// </summary>
internal sealed class UserPlatformSpecificationManager : IUserPlatformSpecificationManager
{
    // Backing fields.
    private ISelectFieldsFromUserPlatformSpecification _selectFieldsFromUserPlatformSpecification;

    public ISelectFieldsFromUserPlatformSpecification SelectFieldsFromUserPlatformSpecification
    {
        get
        {
            _selectFieldsFromUserPlatformSpecification ??= new SelectFieldsFromUserPlatformSpecification();

            return _selectFieldsFromUserPlatformSpecification;
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
