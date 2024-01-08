using System;

namespace FuDeverWebApi.DataAccess.Specifications.Entites.UserHobby.Manager.Contracts;

public interface IUserHobbySpecificationManager
{
    SelectFieldsFromUserHobbySpecification SelectFieldsFromUserHobbySpecification { get; }

    UserHobbyByUserIdSpecification UserHobbyByUserIdSpecification(Guid userId);
}
