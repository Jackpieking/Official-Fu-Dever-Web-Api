using FuDeverWebApi.DataAccess.Specifications.Entites.UserPlatform.Manager.Contracts;
using System;

namespace FuDeverWebApi.DataAccess.Specifications.Entites.UserPlatform.Manager.Implementations;

public sealed class UserPlatformSpecificationManager : IUserPlatformSpecificationManager
{
    private SelectFieldsFromUserPlatformSpecification _selectFieldsFromUserPlatformSpecification;

    public SelectFieldsFromUserPlatformSpecification SelectFieldsFromUserPlatformSpecification
    {
        get
        {
            _selectFieldsFromUserPlatformSpecification ??= new();

            return _selectFieldsFromUserPlatformSpecification;
        }
    }

    public UserPlatformByPlatformIdSpecification UserPlatformByPlatformIdSpecification(Guid platformId)
    {
        return new(platformId: platformId);
    }

    public UserPlatformByUserIdSpecification UserPlatformByUserIdSpecification(Guid userId)
    {
        return new(userId: userId);
    }
}
