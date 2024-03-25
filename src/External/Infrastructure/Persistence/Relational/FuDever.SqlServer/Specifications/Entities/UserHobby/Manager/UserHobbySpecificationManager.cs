using FuDever.Domain.Specifications.Entities.UserHobby;
using FuDever.Domain.Specifications.Entities.UserHobby.Manager;
using System;

namespace FuDever.SqlServer.Specifications.Entities.UserHobby.Manager;

/// <summary>
///     Represent implementation of user hobby specification manager.
/// </summary>
internal sealed class UserHobbySpecificationManager : IUserHobbySpecificationManager
{
    // Backing fields.
    private ISelectFieldsFromUserHobbySpecification _selectFieldsFromUserHobbySpecification;
    private IUserHobbyAsNoTrackingSpecification _userHobbyAsNoTrackingSpecification;
    private IUpdateFieldOfUserHobbySpecification _updateFieldOfUserHobbySpecification;

    public ISelectFieldsFromUserHobbySpecification SelectFieldsFromUserHobbySpecification
    {
        get
        {
            _selectFieldsFromUserHobbySpecification ??= new SelectFieldsFromUserHobbySpecification();

            return _selectFieldsFromUserHobbySpecification;
        }
    }

    public IUserHobbyAsNoTrackingSpecification UserHobbyAsNoTrackingSpecification
    {
        get
        {
            _userHobbyAsNoTrackingSpecification ??= new UserHobbyAsNoTrackingSpecification();

            return _userHobbyAsNoTrackingSpecification;
        }
    }

    public IUpdateFieldOfUserHobbySpecification UpdateFieldOfUserHobbySpecification
    {
        get
        {
            _updateFieldOfUserHobbySpecification ??= new UpdateFieldOfUserHobbySpecification();

            return _updateFieldOfUserHobbySpecification;
        }
    }

    public IUserHobbyByHobbyIdSpecification UserHobbyByHobbyIdSpecification(Guid hobbyId)
    {
        return new UserHobbyByHobbyIdSpecification(hobbyId: hobbyId);
    }

    public IUserHobbyByUserIdSpecification UserHobbyByUserIdSpecification(Guid userId)
    {
        return new UserHobbyByUserIdSpecification(userId: userId);
    }
}
