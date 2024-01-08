using FuDeverWebApi.DataAccess.Specifications.Entites.UserHobby.Manager.Contracts;
using System;

namespace FuDeverWebApi.DataAccess.Specifications.Entites.UserHobby.Manager.Implementations;

public sealed class UserHobbySpecificationManager :
    IUserHobbySpecificationManager
{
    private SelectFieldsFromUserHobbySpecification _selectFieldsFromUserHobbySpecification;

    public SelectFieldsFromUserHobbySpecification SelectFieldsFromUserHobbySpecification
    {
        get
        {
            _selectFieldsFromUserHobbySpecification ??= new();

            return _selectFieldsFromUserHobbySpecification;
        }
    }

    public UserHobbyByUserIdSpecification UserHobbyByUserIdSpecification(
        Guid userId)
    {
        return new(userId: userId);
    }
}
