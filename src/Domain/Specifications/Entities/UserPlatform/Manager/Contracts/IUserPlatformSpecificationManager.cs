using System;

namespace FuDeverWebApi.DataAccess.Specifications.Entites.UserPlatform.Manager.Contracts;

public interface IUserPlatformSpecificationManager
{
    UserPlatformByPlatformIdSpecification UserPlatformByPlatformIdSpecification(Guid platformId);

    UserPlatformByUserIdSpecification UserPlatformByUserIdSpecification(Guid userId);

    SelectFieldsFromUserPlatformSpecification SelectFieldsFromUserPlatformSpecification { get; }
}
